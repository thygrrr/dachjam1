// SPDX-License-Identifier: MIT
// SPDX-FileCopyrightText: 2026 Moritz Voss

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Godot;
namespace tweens.gd;

// Snapshot the effective material slots. Native identity checks are inexpensive; metadata is not scanned per tick.
internal sealed class InstanceShaderWatch : IDisposable
{
    private readonly Node node;
    private readonly Mesh? mesh;
    private readonly Material?[] materials;
    private readonly MaterialWatch?[] bindings;
    internal InstanceShaderWatch(Node node)
    {
        this.node = node;
        mesh = GetMesh(node);
        var count = node is CanvasItem ? 1 : 2 + (mesh?.GetSurfaceCount() ?? 0);
        materials = new Material?[count];
        bindings = new MaterialWatch?[count];
        try
        {
            for (var i = 0; i < count; i++)
            {
                materials[i] = GetMaterial(i);
                if (materials[i] is { } material) bindings[i] = new MaterialWatch(material);
            }
        }
        catch { Dispose(); throw; }
    }
    private static Mesh? GetMesh(Node node) => node switch
    {
        MeshInstance3D instance => instance.Mesh,
        MultiMeshInstance3D instance => instance.Multimesh?.Mesh,
        _ => null,
    };
    private Material? GetMaterial(int slot)
    {
        if (node is CanvasItem canvas)
        {
            while (canvas.UseParentMaterial && canvas.GetParent() is CanvasItem parent) canvas = parent;
            return canvas.Material;
        }
        var geometry = (GeometryInstance3D)node;
        if (slot == 0) return geometry.MaterialOverride;
        if (slot == 1) return geometry.MaterialOverlay;
        return node is MeshInstance3D instance ? instance.GetActiveMaterial(slot - 2) : mesh!.SurfaceGetMaterial(slot - 2);
    }
    internal void Validate()
    {
        var currentMesh = GetMesh(node);
        if (currentMesh != mesh || (mesh is not null && (!GodotObject.IsInstanceValid(mesh) || mesh.GetSurfaceCount() + 2 != materials.Length)))
            throw new InvalidOperationException("The mesh binding changed during shader playback.");
        for (var i = 0; i < materials.Length; i++)
        {
            if (materials[i] is { } original && !GodotObject.IsInstanceValid(original))
                throw new InvalidOperationException("A shader material was disposed during playback.");
            var current = GetMaterial(i);
            if (current != materials[i]) throw new InvalidOperationException("The material binding changed during shader playback.");
            bindings[i]?.Validate();
        }
    }
    public void Dispose()
    {
        foreach (var watch in bindings) watch?.Dispose();
    }

    // Instance parameters may also be declared by a next-pass shader.
    private sealed class MaterialWatch : IDisposable
    {
        private readonly Material material;
        private readonly Material? next;
        private readonly ShaderWatch? shader;
        private readonly MaterialWatch? nextWatch;
        internal MaterialWatch(Material material)
        {
            this.material = material;
            next = material.NextPass;
            try
            {
                if (material is ShaderMaterial m) shader = new ShaderWatch(m.Shader);
                if (next is not null) nextWatch = new MaterialWatch(next);
            }
            catch { Dispose(); throw; }
        }
        internal void Validate()
        {
            if (!GodotObject.IsInstanceValid(material) || material.NextPass != next)
                throw new InvalidOperationException("The material pass binding changed during shader playback.");
            if (material is ShaderMaterial m) shader!.Validate(m.Shader);
            nextWatch?.Validate();
        }
        public void Dispose() { shader?.Dispose(); nextWatch?.Dispose(); }
    }
}
