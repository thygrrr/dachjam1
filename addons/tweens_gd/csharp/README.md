# tweens.gd for C#

Your Godot .NET project compiles these sources in its normal build: no package reference, plugin activation or
autoload is needed. `Generated/` holds the definitions such as `Tweens.Position2D`; keep it with the rest.

```csharp
using tweens.gd;

// From _Ready or later, with the target in the tree.
await sprite.Tween(new Tweens.Position2D((400, 180), 0.6)).End;
```

Get started at https://tweens.gd/csharp/quickstart/

## Using the NuGet package instead

The `tweens.gd` package contains the same library, compiled:

```powershell
dotnet add package tweens.gd
```

If you keep the addon for GDScript, exclude these sources in your game's `.csproj` to avoid duplicate types:

```xml
<ItemGroup>
  <Compile Remove="addons/tweens_gd/csharp/**/*.cs" />
</ItemGroup>
```
