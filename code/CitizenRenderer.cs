using System;
using System.Runtime.InteropServices;
using fennecs;
using Godot;

namespace jam;

/// <summary>
///     Draws every citizen cube linked to it. Subclasses animate the cubes' <see cref="Matrix4X3Custom"/>, this base
///     uploads them. Each renderer works on its own copy of the MultiMesh resource.
/// </summary>
public abstract partial class CitizenRenderer : MultiMeshInstance3D
{
    protected static World World => ECS.World;

    /// <summary>
    ///     Marks a citizen as drawn by this renderer. Always typed as the base, so any owner can be matched.
    /// </summary>
    public Link<CitizenRenderer> Self => Link.With<CitizenRenderer>(this);

    public int Count => _matrices.Count;

    private Stream<Matrix4X3Custom> _matrices;
    private float[] _buffer = [];

    public override void _Ready()
    {
        base._Ready();

        Multimesh = (MultiMesh) Multimesh.Duplicate();
        Multimesh.InstanceCount = 0;
        Multimesh.TransformFormat = MultiMesh.TransformFormatEnum.Transform3D;
        Multimesh.UseColors = false;
        Multimesh.UseCustomData = true;

        _matrices = World.Query<Matrix4X3Custom>().Has(Self).Stream();
    }

    public override void _ExitTree()
    {
        _matrices.Despawn();
        base._ExitTree();
    }

    /// <summary>
    ///     Sizes the MultiMesh for up to <paramref name="capacity"/> cubes within <paramref name="bounds"/>.
    /// </summary>
    public void Allocate(int capacity, Aabb bounds)
    {
        Multimesh.InstanceCount = capacity;
        Multimesh.VisibleInstanceCount = 0;
        Multimesh.CustomAabb = bounds;
        _buffer = new float[capacity * Matrix4X3Custom.SizeInFloats];
    }

    /// <summary>
    ///     Takes the citizen over from whichever renderer drew it before.
    /// </summary>
    public void Adopt(Entity citizen) => citizen.Remove<CitizenRenderer>(Match.Object).Add(Self);

    public override void _Process(double delta)
    {
        base._Process(delta);
        Animate((float) (Time.GetTicksUsec() * 1e-6), (float) delta);
        Upload();
    }

    protected abstract void Animate(float time, float delta);

    // Concatenates all archetypes' matrices, so the MultiMesh can be uploaded in one call.
    private void Upload()
    {
        if (_buffer.Length == 0) return;

        var written = 0;
        _matrices.Raw(memory =>
        {
            var floats = MemoryMarshal.Cast<Matrix4X3Custom, float>(memory.Span);
            floats.CopyTo(_buffer.AsSpan(written));
            written += floats.Length;
        });

        RenderingServer.MultimeshSetBuffer(Multimesh.GetRid(), _buffer);
        Multimesh.VisibleInstanceCount = written / Matrix4X3Custom.SizeInFloats;
    }
}
