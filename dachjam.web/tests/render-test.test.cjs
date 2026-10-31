const assert = require('node:assert/strict');
const { readFileSync } = require('node:fs');
const { test } = require('node:test');
const vm = require('node:vm');

const html = readFileSync(`${__dirname}/../wwwroot/index.html`, 'utf8');
const config = html.match(/const renderTest = [\s\S]*?const GODOT_THREADS_ENABLED = false;/)[0];

test('rendering probes use startup arguments for Mobile and user arguments for runtime tests', () => {
    for (const [search, expected] of [
        ['', []],
        ['?2dog-timing', []],
        ['?render-test=unknown', []],
        ['?render-test=mobile', ['--rendering-method', 'mobile']],
        ['?render-test=no-msaa', ['--', '--render-test=no-msaa']],
        ['?render-test=unshaded', ['--', '--render-test=unshaded']],
    ]) {
        const args = vm.runInNewContext(`${config}\nJSON.stringify(GODOT_CONFIG.args)`, {
            URLSearchParams, location: { search },
        });
        assert.deepEqual(JSON.parse(args), expected);
    }
});
