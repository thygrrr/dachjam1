using Godot;
using Engine = twodog.Engine;

internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        Console.WriteLine("dachjam (web) starting...");

        // The Godot project's assembly owns the source-generated plugins
        // initializer; register it before Start() (there is no
        // GodotPlugins.dll on web).
        Engine.RegisterWebPluginsInitializer(TwoDogWebBoot.PluginsInitializer());

        // args come from the page's GODOT_CONFIG.args plus the
        // '--main-pack godot.pck' the engine loader prepends.
        var engine = new Engine("dachjam", args: args);
        try
        {
            engine.Start();
            var renderTests = OS.GetCmdlineUserArgs();
            if (renderTests.Contains("--render-test=no-msaa")) engine.Tree.Root.Msaa3D = Viewport.Msaa.Disabled;
            if (renderTests.Contains("--render-test=unshaded")) engine.Tree.Root.DebugDraw = Viewport.DebugDrawEnum.Unshaded;
            Console.WriteLine($"Rendering diagnostics: renderer={RenderingServer.GetCurrentRenderingMethod()}, MSAA={engine.Tree.Root.Msaa3D}, debug={engine.Tree.Root.DebugDraw}, prepass={ProjectSettings.GetSetting("rendering/driver/depth_prepass/enable").AsBool()}");
            GD.Print("2dog is running in the browser!");
            GD.Print("Scene Root: ", engine.Tree.CurrentScene.Name);
            // The browser loop owns the lifetime after Run() returns.
            engine.Run();
        }
        catch
        {
            await engine.DisposeAsync();
            throw;
        }

        return 0;
    }
}
