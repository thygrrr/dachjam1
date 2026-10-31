// Run with: node --test dachjam.web/tests/focus.test.cjs
const assert = require('node:assert/strict');
const { readFileSync } = require('node:fs');
const { test } = require('node:test');
const vm = require('node:vm');

const html = readFileSync(`${__dirname}/../wwwroot/index.html`, 'utf8');
const recovery = html.match(/function enableFocusRecovery\(\) \{[\s\S]*?\n            \}/)[0];

function surface() {
    const listeners = new Map();
    return {
        addEventListener(type, callback) {
            if (!listeners.has(type)) listeners.set(type, []);
            listeners.get(type).push(callback);
        },
        emit(type, target) {
            const event = { target, prevented: false, preventDefault() { this.prevented = true; } };
            for (const callback of listeners.get(type) || []) callback(event);
            return event;
        },
    };
}

function setup(hasFocus = true) {
    const hint = { ...surface(), hidden: true };
    const document = { ...surface(), body: {}, documentElement: {}, hidden: false, hasFocus: () => hasFocus };
    const window = surface();
    let focusCalls = 0;
    const canvas = {
        focus(options) {
            assert.equal(options.preventScroll, true);
            focusCalls++;
            hasFocus = true;
            document.activeElement = canvas;
            document.emit('focusin', canvas);
        },
    };
    document.activeElement = document.body;
    document.getElementById = id => id === 'canvas' ? canvas : hint;
    const microtasks = [];
    vm.runInNewContext(`(${recovery})()`, { document, window, queueMicrotask: callback => microtasks.push(callback) });
    return {
        document, window, canvas, hint,
        get focusCalls() { return focusCalls; },
        setFocused(value) { hasFocus = value; },
        flush() { while (microtasks.length) microtasks.shift()(); },
    };
}

test('inline scripts parse; startup respects an unfocused iframe', () => {
    for (const [, script] of html.matchAll(/<script>([\s\S]*?)<\/script>/g)) new vm.Script(script);
    const focused = setup();
    assert.equal(focused.document.activeElement, focused.canvas);
    assert.equal(focused.hint.hidden, true);
    const unfocused = setup(false);
    assert.equal(unfocused.focusCalls, 0);
    assert.equal(unfocused.hint.hidden, false);
});

test('both letterbox targets recover focus without intercepting controls or canvas input', () => {
    const app = setup(false);
    for (const target of [app.document.body, app.document.documentElement]) {
        app.document.activeElement = app.document.body;
        assert.equal(app.document.emit('pointerdown', target).prevented, true);
        assert.equal(app.document.activeElement, app.canvas);
        assert.equal(app.hint.hidden, true);
    }
    const calls = app.focusCalls;
    for (const target of [app.canvas, {}, app.hint]) {
        assert.equal(app.document.emit('pointerdown', target).prevented, false);
    }
    assert.equal(app.focusCalls, calls);
});

test('hint detects parent focus loss even when canvas stays active and recovers on click', () => {
    const app = setup();
    app.setFocused(false);
    app.window.emit('blur');
    assert.equal(app.document.activeElement, app.canvas);
    assert.equal(app.hint.hidden, false);
    app.hint.emit('click');
    assert.equal(app.hint.hidden, true);

    app.document.emit('focusout');
    app.document.activeElement = {};
    app.flush();
    assert.equal(app.hint.hidden, false);
});

test('return and fullscreen recovery respect document focus, visibility, controls and IME', () => {
    const app = setup();
    for (const [surface, event] of [
        [app.window, 'focus'],
        [app.document, 'visibilitychange'],
        [app.document, 'fullscreenchange'],
    ]) {
        for (const active of [app.document.body, app.document.documentElement, app.canvas]) {
            app.document.activeElement = active;
            surface.emit(event);
            assert.equal(app.document.activeElement, app.canvas);
        }
        for (const active of [{ tagName: 'INPUT' }, { tagName: 'BUTTON' }, { contentEditable: true }]) {
            app.document.activeElement = active;
            const calls = app.focusCalls;
            surface.emit(event);
            assert.equal(app.focusCalls, calls);
            assert.equal(app.document.activeElement, active);
        }
        app.document.activeElement = app.document.body;
        app.setFocused(false);
        const calls = app.focusCalls;
        surface.emit(event);
        assert.equal(app.focusCalls, calls);
        app.setFocused(true);
        app.document.hidden = true;
        surface.emit(event);
        assert.equal(app.focusCalls, calls);
        assert.equal(app.hint.hidden, true);
        app.document.hidden = false;
    }
});
