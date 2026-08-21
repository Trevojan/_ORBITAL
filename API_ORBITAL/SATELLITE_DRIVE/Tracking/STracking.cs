using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SATELLITE_CORE.Constellation;
using SATELLITE_ENGINE.Satellite;

namespace SATELLITE_DRIVE.Tracking;

/// <summary>Domain to wire. Knows both shapes; owns neither's rules.</summary>
public sealed class STrackingTranslator : ITrackingTranslator
{
    public RSatelliteResponse Describe(ISatellite satellite)
    {
        ArgumentNullException.ThrowIfNull(satellite);

        return new RSatelliteResponse(
            satellite.Designator,
            satellite.AltitudeKm,
            satellite.InclinationDeg,
            satellite.State.ToString());
    }

    public RConstellationResponse Describe(MConstellation constellation)
    {
        ArgumentNullException.ThrowIfNull(constellation);

        return new RConstellationResponse(
            constellation.Designator,
            constellation.Health.ToString(),
            constellation.Count,
            [.. constellation.Members.Select(Describe)]);
    }
}

/// <summary>
/// The HTTP surface. Every handler is a translation plus a delegation —
/// if a rule ever appears in this file, it is in the wrong project.
/// </summary>
public sealed class STrackingEndpoints : ITrackingEndpoints
{
    public IEndpointRouteBuilder Map(IEndpointRouteBuilder routes)
    {
        ArgumentNullException.ThrowIfNull(routes);

        var group = routes
            .MapGroup(StrTracking.GroupPrefix)
            .WithTags(StrTracking.GroupTag);

        group.MapPost(StrTracking.ClassifyRoute, Classify).WithName("ClassifySatellite");
        group.MapPost(StrTracking.ConstellationRoute, Assess).WithName("AssessConstellation");

        return routes;
    }

    private static IResult Classify(
        RSatelliteRequest request,
        ISatelliteOperations satellites,
        ITrackingTranslator translator) =>
        TrackOne(request, satellites, translator)
            .Match(payload => Results.Ok(payload), problem => Results.BadRequest(problem));

    private static IResult Assess(
        string designator,
        RSatelliteRequest[] requests,
        ISatelliteOperations satellites,
        IConstellationOperations constellations,
        ITrackingTranslator translator)
    {
        if (requests.Length is 0)
        {
            return Results.BadRequest(
                new RTrackingProblem(ETrackingOutcome.NotTracked.ToString(), "No members supplied."));
        }

        var members = requests.Select(request =>
            MSatellite.Track(request.Designator, request.AltitudeKm, request.InclinationDeg));

        var assessed = constellations.Refresh(
            MConstellation.Named(designator, members),
            StrTracking.AssumedContactGap);

        return Results.Ok(translator.Describe(assessed));
    }

    private static MTrackingResult<RSatelliteResponse> TrackOne(
        RSatelliteRequest request,
        ISatelliteOperations satellites,
        ITrackingTranslator translator)
    {
        if (string.IsNullOrWhiteSpace(request.Designator))
        {
            return MTrackingResult<RSatelliteResponse>.Rejected("Designator is required.");
        }

        if (!satellites.IsAltitudeSustainable(request.AltitudeKm))
        {
            return MTrackingResult<RSatelliteResponse>.Rejected(
                $"Altitude {request.AltitudeKm} km falls outside the trackable band.");
        }

        var satellite = MSatellite.Track(request.Designator, request.AltitudeKm, request.InclinationDeg);
        var classified = satellite.At(satellites.Classify(satellite, StrTracking.AssumedContactGap));

        return MTrackingResult<RSatelliteResponse>.Served(translator.Describe(classified));
    }
}
