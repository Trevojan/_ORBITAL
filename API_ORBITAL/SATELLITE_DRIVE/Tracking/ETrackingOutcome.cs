namespace SATELLITE_DRIVE.Tracking;

/// <summary>
/// What an endpoint decided, expressed in the _DRIVE's own vocabulary.
/// Deliberately not HTTP status codes — the mapping to those happens at the edge.
/// </summary>
public enum ETrackingOutcome
{
    Unknown = 0,
    Served,
    Rejected,
    NotTracked
}
