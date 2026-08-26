namespace BEACON_DRIVE.Uplink;

/// <summary>
/// The wire contract, declared here rather than referenced from the API (AR6). The two sides
/// agree on a shape; they do not share a type. A breaking change surfaces as a
/// deserialization failure at this boundary instead of as a compile error in a project
/// neither team owns.
/// </summary>
public sealed record RTrackSatellite(string Designator, double AltitudeKm, double InclinationDeg);

public sealed record RTrackedSatellite(string Designator, double AltitudeKm, double InclinationDeg, string State);

public sealed record RUplinkFault(string Outcome, string Detail);
