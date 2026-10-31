"""Confirm DXC's inout/row_major matrix bug and three controls, without a GPU."""
import re
import subprocess
import sys
import tempfile
from pathlib import Path

UNIFORM = [2, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 0, 0, 0, 0, 1,
           1, 0, 0, -2, 0, 1, 0, -6, 0, 0, 1, -4, 0, 0, 0, 0]


def evaluate(path):
    """Only the straight-line instructions used by this shader; reject anything else."""
    values = {}
    def number(token):
        return values[token] if token.startswith('%') else float(token)
    for line in Path(path).read_text().splitlines():
        if '@dx.op.bufferStore.i32(' in line and line.lstrip().startswith('call '):
            ids = re.findall(r'i32 (%\d+)', line)
            assert len(ids) == 4, line
            return [values[key] for key in ids]
        match = re.match(r'\s*(%\d+) = (.*?)(?:\s*;.*)?$', line)
        if not match:
            continue
        key, instruction = match.groups()
        if '@dx.op.createHandle(' in instruction:
            values[key] = None
        elif '@dx.op.cbufferLoadLegacy.f32(' in instruction:
            register = int(re.search(r'i32 (\d+)\)$', instruction)[1])
            values[key] = UNIFORM[register * 4:register * 4 + 4]
        elif instruction.startswith('extractvalue '):
            source, index = re.search(r'(%\d+), (\d+)$', instruction).groups()
            values[key] = values[source][int(index)]
        elif instruction.startswith(('fmul ', 'fadd ')):
            left, right = re.search(r'float ([^,]+), (.+)$', instruction).groups()
            values[key] = number(left) * number(right) if instruction.startswith('fmul') else number(left) + number(right)
        elif '@dx.op.tertiary.f32(i32 46,' in instruction:
            a, b, c = re.findall(r'float ([%\d+.-][^,) ]*)', instruction)
            values[key] = number(a) * number(b) + number(c)
        elif instruction.startswith('bitcast float '):
            values[key] = values[re.search(r'(%\d+) to i32$', instruction)[1]]
        else:
            raise AssertionError(instruction)
    raise AssertionError('No output store found')


def check(compiler):
    original = Path(__file__).with_name('camera.hlsl').read_text()
    body = re.search(r'float4 v_1_\(inout Camera_1_ camera\)\s*\{([\s\S]*?)\n\}', original)[1]
    cases = [
        ('copied', original, [], [-2, -15, -10, 1]),
        ('input-only', original.replace('inout Camera_1_ camera', 'in Camera_1_ camera'), [], [2, -12, -15, 1]),
        ('inline', original.replace('const float4 _e14 = v_1_(param);', body.replace('camera.', 'param.').replace('return ', 'const float4 _e14 = ')), [], [2, -12, -15, 1]),
        ('row-major', original, ['-Zpr'], [2, -12, -15, 1]),
    ]
    with tempfile.TemporaryDirectory(prefix='camera-copy-') as directory:
        for name, source, flags, expected in cases:
            path = Path(directory) / f'{name}.hlsl'
            assembly = path.with_suffix('.txt')
            path.write_text(source)
            subprocess.run([compiler, '-Vd', '-HV', '2018', '-Ges', '-T', 'cs_6_0', '-E', 'main',
                            '-Fc', str(assembly), '-Fo', str(path.with_suffix('.dxil')), *flags, str(path)], check=True)
            actual = evaluate(assembly)
            print(f'{name}: {actual}', flush=True)
            assert actual == expected, f'{name}: behavior changed, expected {expected}; the compiler may have been fixed'
    print('Known DXC failure reproduced; all three controls produce correct coordinates.')


if __name__ == '__main__':
    assert len(sys.argv) == 2, 'Usage: python check_dxc.py <path-to-dxc.exe>'
    check(sys.argv[1])
