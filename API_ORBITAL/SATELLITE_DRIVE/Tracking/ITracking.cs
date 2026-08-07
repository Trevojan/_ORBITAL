using Microsoft.AspNetCore.Routing;
using SATELLITE_CORE.Constellation;
using SATELLITE_ENGINE.Satellite;

namespace SATELLITE_DRIVE.Tracking;

/// <summary>Publishes the concept's HTTP surface. The host calls this and learns nothing else.</summary>
public interface ITrackingEndpoints
{
    IEndpointRouteBuilder Map(IEndpointRouteBuilder routes);
}

/// <summary>
/// Turns domain models into wire records. Split from the endpoints on purpose —
/// translation is testable without spinning up a server.
/// </summary>
public interface ITrackingTranslator
{
    RSatelliteResponse Describe(ISatellite satellite);

    RConstellationResponse Describe(MConstellation constellation);
}
