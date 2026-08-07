using DESKTOP_PANEL.Shell;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;
using UPLINK_DRIVE.Uplink;
using VIEW_CORE.Readout;
using VIEW_ENGINE.Window;
using ViewApp = VIEW_CORE.App;

namespace DESKTOP_PANEL;

/// <summary>
/// The desktop composition root — the only file in DESKTOP_ORBITAL that names
/// every project at once.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

        appBuilder.Services.AddLogging();

        // _ENGINE — pure sizing rules.
        appBuilder.Services.AddSingleton<IWindowOperations, SWindow>();

        // _CORE — how a reading should be presented.
        appBuilder.Services.AddScoped<IReadoutOperations, SReadout>();

        // _DRIVE — the outbound client. The key is resolved here, at the root,
        // from the environment rather than compiled into any project.
        var apiKey = Environment.GetEnvironmentVariable(StrShell.KeyVariable) ?? StrShell.DevelopmentKey;

        appBuilder.Services.AddHttpClient<IUplinkOperations, SUplink>(client =>
        {
            client.BaseAddress = StrUplink.DefaultBaseAddress;
            client.Timeout = StrUplink.CallTimeout;
            client.DefaultRequestHeaders.Add(StrUplink.KeyHeader, apiKey);
        });

        // _PANEL — the join: VIEW_CORE's contract, satisfied by UPLINK_DRIVE.
        appBuilder.Services.AddScoped<IReadoutSource, SShell>();
        appBuilder.Services.AddSingleton<IShellOperations, SShellState>();

        appBuilder.RootComponents.Add<ViewApp>("app");

        var app = appBuilder.Build();

        // Sizing comes from the engine, so the host hardcodes no dimensions.
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
