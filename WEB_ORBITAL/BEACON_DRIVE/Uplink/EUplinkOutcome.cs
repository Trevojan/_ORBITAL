namespace BEACON_DRIVE.Uplink;

/// <summary>
/// What happened to a call, in the page's vocabulary rather than HTTP's.
/// The distinction that matters to a reader is "the API said no" versus "the API is not there".
/// </summary>
public enum EUplinkOutcome
{
    Unknown = 0,
    Reached,
    Refused,
    Unreachable,
    Malformed
}
