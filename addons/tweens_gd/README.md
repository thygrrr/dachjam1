# tweens.gd

Tweens for GDScript and C#: reusable definitions, composable easing, and playback you can pause, cancel and
await. Both APIs are in beta and may change. Documentation: https://tweens.gd

## Install

Keep this whole `addons/tweens_gd/` folder in your project and let the editor finish importing it.
No plugin activation or autoload is needed. Requires Godot 4.7 or later (tested with 4.7.2).
For Web exports, turn on **Extensions Support** in the export preset.

## GDScript

```gdscript
extends Sprite2D

func _unhandled_input(event: InputEvent) -> void:
	if event is InputEventMouseButton and event.pressed:
		Tweens.play(self, Tweens.position_2d(event.position, 1.2, Out.BACK))
```

`Tweens.play()` returns a handle to `pause()`, `resume()`, `cancel()` or `await handle.end`.
[CATALOG.md](CATALOG.md) lists every property helper. Get started at https://tweens.gd/gdscript/quickstart/

## C#

Needs Godot .NET and .NET 8 or later; your project's build compiles `csharp/`.

```csharp
using Godot;
using tweens.gd;

public partial class ClickToMove : Sprite2D
{
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton { Pressed: true } click)
        {
            this.TweenPosition(click.Position, 1.2, Out.Back);
        }
    }
}
```

Get started at https://tweens.gd/csharp/quickstart/. To use the NuGet package instead, see
[csharp/README.md](csharp/README.md). GDScript-only projects can keep the `csharp/` folder; it needs no .NET.

## License

MIT, see [LICENSE](LICENSE). Third-party code is credited in [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md).
