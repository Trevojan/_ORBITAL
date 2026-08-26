using BEACON_CORE.Readout;
using BEACON_DRIVE.Uplink;
using BEACON_ENGINE.Viewport;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WEB_PANEL.Shell;

// The web composition root. The only file in WEB_ORBITAL that names every project at once.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<Shell>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// _ENGINE - pure layout rules.
builder.Services.AddSingleton<IViewportOperations, SViewport>();

// _CORE - how a reading is presented.
builder.Services.AddScoped<IReadoutOperations, SReadout>();

// _DRIVE - the outbound client. The base address is the origin the page was served from,
// so the app calls back to whoever served it and there is no cross-origin request to
// configure. Pointing this elsewhere is what makes CORS a problem.
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
    Timeout = StrUplink.CallTimeout
});

builder.Services.AddScoped<IUplinkOperations, SUplink>();

// _PANEL - the join: BEACON_CORE's contract, satisfied by BEACON_DRIVE.
builder.Services.AddScoped<IReadoutSource, SShell>();

await builder.Build().RunAsync();
