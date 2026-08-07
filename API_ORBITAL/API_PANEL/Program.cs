using API_PANEL.Access;
using RELAY_CORE.Channel;
using RELAY_ENGINE.Envelope;
using SATELLITE_CORE.Constellation;
using SATELLITE_DRIVE.Tracking;
using SATELLITE_ENGINE.Satellite;
using VAULT_CORE.Ledger;
using VAULT_ENGINE.Medallion;

// The composition root. This is the only file in API_ORBITAL that names
// every project at once — which is precisely a _PANEL's job.
var builder = WebApplication.CreateBuilder(args);

// _ENGINE — pure logic, no dependencies of its own.
builder.Services.AddSingleton<ISatelliteOperations, SSatellite>();
builder.Services.AddSingleton<IEnvelopeOperations, SEnvelope>();
builder.Services.AddSingleton<IMedallionOperations, SMedallion>();

// _CORE — orchestration, each over its own engine.
builder.Services.AddSingleton<IConstellationOperations, SConstellation>();
builder.Services.AddSingleton<IChannelOperations, SChannel>();
builder.Services.AddSingleton<ILedgerOperations, SLedger>();

// _DRIVE — the wire surface.
builder.Services.AddSingleton<ITrackingTranslator, STrackingTranslator>();
builder.Services.AddSingleton<ITrackingEndpoints, STrackingEndpoints>();

// _PANEL — authentication, owned here and nowhere else.
builder.Services.AddSingleton<IAccessPolicy, SAccess>();
builder.Services.AddSingleton<SAccessGate>();

var app = builder.Build();

var api = app
    .MapGroup(StrAccess.ApiPrefix)
    .AddEndpointFilter<SAccessGate>();

// The _DRIVE publishes its own routes; the panel never spells them out.
app.Services.GetRequiredService<ITrackingEndpoints>().Map(api);

// Proof of composition: three apps answering one request, none of them
// holding a reference to another.
api.MapGet("/health", (IChannelOperations channels, ILedgerOperations ledgers) =>
{
    var channel = MChannel.Between(nameof(API_PANEL), "DATA_ORBITAL");
    var ledger = MLedger.Opened(nameof(API_PANEL), DateTimeOffset.UtcNow);

    return Results.Ok(new
    {
        Relay = channels.StateOf(channel).ToString(),
        Vault = ledgers.TallyOf(ledger),
        Tracking = "ready"
    });
})
.WithName("Health");

app.Run();
