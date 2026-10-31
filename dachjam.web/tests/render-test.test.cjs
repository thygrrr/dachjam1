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
        ['?render-test=depth', []],
        ['?render-test=no-prepass', []],
        ['?render-test=direct-camera', []],
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

test('direct camera probe bypasses local camera copies only in vertex shaders', () => {
    const probe = html.match(/if \(renderTest === 'direct-camera'\) \{[\s\S]*?\n        \}/)[0];
    const received = [];
    const logs = [];
    class GPUDevice {
        createShaderModule(descriptor) { received.push(descriptor); return descriptor; }
    }
    vm.runInNewContext(probe, {
        renderTest: 'direct-camera', GPUDevice, console: { log: line => logs.push(line) },
    });
    const device = new GPUDevice();
    const source = '@vertex var<uniform> scene_data_block; (*(scene_data)).view_matrix; (*(scene_data)).projection_matrix;';
    const descriptor = { code: source, label: 'camera' };
    const changed = device.createShaderModule(descriptor);
    assert.equal(changed.code, '@vertex var<uniform> scene_data_block; scene_data_block.data.view_matrix; scene_data_block.data.projection_matrix;');
    assert.equal(changed.label, 'camera');
    assert.equal(descriptor.code, source);
    device.createShaderModule(descriptor);
    assert.equal(logs.length, 1);
    for (const code of [source.replace('@vertex', '@fragment'), '@vertex fn main() {}']) {
        const untouched = { code };
        assert.equal(device.createShaderModule(untouched), untouched);
    }
    assert.equal(received.length, 4);
});

test('depth trace reports unique states and forwards GPU calls unchanged', async () => {
    const trace = html.match(/if \(renderTest === 'depth'[^\n]*\) \{[\s\S]*?\n        \}/)[0];
    const calls = [];
    const logs = [];
    const pipeline = {};
    const pass = {};
    const asyncPipeline = Promise.resolve(pipeline);
    class GPUDevice {
        createRenderPipeline(descriptor) { calls.push(descriptor); return pipeline; }
        createRenderPipelineAsync(descriptor) { calls.push(descriptor); return asyncPipeline; }
    }
    class GPUCommandEncoder {
        beginRenderPass(descriptor) { calls.push(descriptor); return pass; }
    }
    class GPURenderPassEncoder {
        setViewport(...args) { calls.push(args); }
    }
    vm.runInNewContext(trace, {
        renderTest: 'depth', GPUDevice, GPUCommandEncoder, GPURenderPassEncoder,
        console: { log: line => logs.push(line) },
    });
    const descriptor = {
        depthStencil: { depthCompare: 'greater-equal', depthWriteEnabled: true, format: 'depth32float' },
        multisample: { count: 4 },
    };
    const device = new GPUDevice();
    assert.equal(device.createRenderPipeline(descriptor), pipeline);
    assert.equal(device.createRenderPipelineAsync(descriptor), asyncPipeline);
    assert.equal(await asyncPipeline, pipeline);
    assert.deepEqual(calls.slice(0, 2), [descriptor, descriptor]);
    assert.equal(logs.length, 1);
    const renderPass = { depthStencilAttachment: { depthLoadOp: 'clear', depthClearValue: 0 } };
    assert.equal(new GPUCommandEncoder().beginRenderPass(renderPass), pass);
    assert.equal(calls[2], renderPass);
    assert.match(logs[1], /"clear":0/);
    new GPURenderPassEncoder().setViewport(0, 0, 800, 800, 0, 1);
    assert.deepEqual(calls[3], [0, 0, 800, 800, 0, 1]);
    assert.match(logs[2], /"min":0,"max":1/);
});

test('only no-prepass queues a startup override before the engine starts', () => {
    const makeEngine = html.match(/const makeEngine = \(suffix\) => \{[\s\S]*?\n                    \};/)[0];
    for (const renderTest of [null, 'depth', 'mobile', 'no-prepass']) {
        const files = [];
        class Engine {
            preloadFile(buffer, path) { files.push({ text: new TextDecoder().decode(buffer), path }); }
        }
        const engine = vm.runInNewContext(`${makeEngine}\nmakeEngine('')`, {
            Engine, renderTest, TextEncoder, GODOT_CONFIG: {}, fileSizes: {}, engineProgress() {},
        });
        assert.ok(engine instanceof Engine);
        assert.deepEqual(files, renderTest === 'no-prepass' ? [{
            text: '[rendering]\ndriver/depth_prepass/enable=false\n', path: 'override.cfg',
        }] : []);
    }
});
