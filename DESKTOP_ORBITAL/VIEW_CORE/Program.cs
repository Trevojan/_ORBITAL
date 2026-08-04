using System;
using Microsoft.Extensions.DependencyInjection;
using VIEW_ENGINE;


namespace VIEW_CORE
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);
            appBuilder.Services
                .AddLogging();

            appBuilder.RootComponents.Add<App>("app");

            var app = appBuilder.Build();

            app.MainWindow
                .SetIconFile("favicon.ico")
                .SetTitle("Photino Hello World");

            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
            };

            app.Run();
        }

    }
}
