namespace UPLINK_DRIVE.Uplink;

/// <summary>
/// What happened to a call, in the desktop's vocabulary rather than HTTP's.
/// The distinction that matters to a user is "the API said no" versus "the API is not there".
/// </summary>
public enum EUplinkOutcome
{
    Unknown = 0,
    Reached,
    Refused,
    Unreachable,
    Malformed
}
