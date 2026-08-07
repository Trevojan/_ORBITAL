using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;
using VIEW_ENGINE.Window;

namespace VIEW_CORE;

/// <summary>
/// The desktop composition root. It decides how the app is wired;
/// how a window should be sized is VIEW_ENGINE's business, not this file's.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

        appBuilder.Services.AddLogging();
        appBuilder.Services.AddSingleton<IWindowOperations, SWindow>();

        appBuilder.RootComponents.Add<App>("app");

        var app = appBuilder.Build();

        // Sizing rules come from the engine, so the host never hardcodes a number.
        var window = new SWindow().Normalize(MWindow.Named(StrWindow.DefaultTitle));

        app.MainWindow
            .SetIconFile("favicon.ico")
            .SetTitle(window.Title)
            .SetWidth(window.Width)
            .SetHeight(window.Height)
            .SetResizable(window.IsResizable);

        AppDomain.CurrentDomain.UnhandledException += (_, error) =>
            app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());

        app.Run();
    }
}
