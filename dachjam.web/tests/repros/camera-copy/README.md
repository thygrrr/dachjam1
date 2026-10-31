# Firefox Forward+ camera corruption: root cause

The failure is reproducible without Godot, game code, camera movement, depth testing, MSAA or a GPU driver. Microsoft DXC miscompiles an HLSL `inout` struct argument containing a `row_major` matrix when compiling with the default column-major setting. Naga emits this argument for a WGSL `ptr<function, Camera>` parameter.

## Evidence collected on 2026-10-31

- Firefox 159.0, build `20261022150525`, source `5add844c26613d7e4d84c799d6e23cf987b1240d`.
- Its release Cargo.lock pins Naga 30.0.0 / wgpu `4f4dc63098fae64a0e6d1d3dc6f4f328da8fd5c8`.
- The shader was compiled from GLSL to SPIR-V 1.3 with 2dog's bundled glslang, then to WGSL with its bundled Tint. The WGSL was validated and translated to HLSL using that exact Naga revision.
- In the independent browser test, Firefox passed 6/12 checks: copied cameras failed in both compute and vertex stages, for small and full SceneData structures, including previous-frame data. All corresponding direct/optimized reads passed. Chrome 156 passed 12/12.
- The copied-camera result was `[-2, -15, -10, 1]`, expected `[2, -12, -15, 1]`. Previous-frame results were `[-105, -77, -65, 1]`, expected `[-42, -66, -78, 1]`.
- Compiling `camera.hlsl` with Firefox's own `dxcompiler.dll` 1.9.0.0 produces DXIL that computes exactly `[-2, -15, -10, 1]`. The Windows SDK DXC 1.9.2602.17 does too. This is visible in the instructions before execution on any GPU.
- Keeping everything else equal, replacing `inout` with `in`, inlining the helper, or compiling with `-Zpr` produces the expected result. FXC also produces equivalent correct instructions for the copied and optimized variants.

The bad DXIL uses the wrong matrix components. This explains the apparently misplaced camera even though the CPU camera position and aim are correct. Reversed-Z state and long-frame camera updates were distractions. A current-frame-only shader substitution can also break previous-frame transforms, so the diagnostic URL bypass is not a suitable fix.

## Why this became visible now

Godot's Forward+ shader has passed SceneData by value since upstream commit `14b60f22644` (2025-06-23). Mobile passes individual camera matrices/scalars instead and avoids this struct argument.

In the local 2dog fork, commit `4813ebf2e186948819086d467b9d6e924b36c881` (2026-10-27 23:02 +0100), pinned by 2dog commit `39e69a81`, removed the automatic Mobile fallback for WebGPU adapters lacking `texture-formats-tier2`. Its parent explicitly selected Mobile for that case. The commit specifically identifies Firefox's wgpu implementation among the affected adapters. Keeping Forward+ enabled exposed this previously hidden shader-compiler defect.

That is the source-level exposure mechanism. We cannot establish which renderer the older successful Linux run actually used without its log; Linux also takes a different shader backend from Windows HLSL/DXC.

## Reproduce

Serve `index.html` over localhost or HTTPS and open it in Firefox and Chrome. It retains the two small shader cases from the tested twelve-case page, in compute and vertex stages. Each check reads back actual GPU results. No game assets or runtime are needed. The original twelve-case page and intermediate compiler artifacts remain in `TestResults/camera-copy` in this workspace.

For the compiler-only check, run:

```powershell
python dachjam.web/tests/repros/camera-copy/check_dxc.py <path-to-dxc.exe>
```

The script needs a matching `dxcompiler.dll` beside `dxc.exe`. The investigation used the installed Windows SDK executable with a **copy** of Firefox's DLL in an isolated directory. No installed browser files were changed. `-Vd` skips container validation so that no separate validator DLL is required; this does not change shader optimization. The compiler flags otherwise match wgpu's HLSL 2018 and strictness settings.

The script checks the generated straight-line instructions rather than relying on rendering. It asserts the known bad result and three controls. If the compiler has been fixed, its initial assertion will instead report that the known failure no longer reproduces.

## Shared fix boundary

The defect belongs in DXC, with a possible general mitigation in Firefox/wgpu's HLSL compilation or Naga's pointer-argument lowering. `-Zpr` is a small candidate and fixes this repro, but needs the backend's broader shader tests before adoption. An engine-side alternative is to inline shader helpers before Tint, preserving both current and previous-frame data. Neither option has been shipped or validated across the full engine here. A camera-specific text replacement or switching the demo to Mobile would hide the defect.

The narrow engine candidate has also been checked through the actual compiler chain: SPIRV-Tools merge-return, exhaustive-inlining and dead-function elimination → bundled Tint → Firefox's exact Naga revision → Firefox's bundled DXC. It computes `[2, -12, -15, 1]` correctly, even with the local Camera structure copies still present. Thus the required change in `freeze_and_inline()` is to replace `CreateInlineOpaquePass()` with `CreateInlineExhaustivePass()`, retaining the existing merge-return and cleanup passes. This removes the bad helper argument boundary without substituting current-frame uniforms for previous-frame values. Full shader-corpus, browser and performance checks remain necessary. Release exports must rebake WGSL with the updated engine; the persistent cache already includes the engine commit hash.

No upstream issue has been submitted. These artifacts are ready to attach to a DXC/Firefox/wgpu report.
