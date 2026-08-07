namespace SATELLITE_DRIVE.Tracking;

/// <summary>
/// The wire contract. Records, not models: these exist to be serialized and may
/// change shape for API-versioning reasons without disturbing the domain.
/// </summary>
public sealed record RSatelliteRequest(string Designator, double AltitudeKm, double InclinationDeg);

public sealed record RSatelliteResponse(string Designator, double AltitudeKm, double InclinationDeg, string State);

public sealed record RConstellationResponse(
    string Designator,
    string Health,
    int Count,
    IReadOnlyList<RSatelliteResponse> Members);

public sealed record RTrackingProblem(string Outcome, string Detail);
