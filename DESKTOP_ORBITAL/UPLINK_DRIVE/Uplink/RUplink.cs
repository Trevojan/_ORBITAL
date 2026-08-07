namespace UPLINK_DRIVE.Uplink;

/// <summary>
/// The desktop's own copy of the wire contract. It mirrors API_ORBITAL's records by
/// agreement, not by reference: the two can version apart, and a breaking change on
/// either side shows up here as a deserialization failure rather than a compile error
/// in a shared project nobody owns.
/// </summary>
public sealed record RTrackSatellite(string Designator, double AltitudeKm, double InclinationDeg);

public sealed record RTrackedSatellite(string Designator, double AltitudeKm, double InclinationDeg, string State);

public sealed record RTrackedConstellation(
    string Designator,
    string Health,
    int Count,
    IReadOnlyList<RTrackedSatellite> Members);

public sealed record RUplinkFault(string Outcome, string Detail);
