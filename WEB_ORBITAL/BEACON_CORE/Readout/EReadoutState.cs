namespace BEACON_CORE.Readout;

/// <summary>What the page should show, derived by <see cref="SReadout"/>.</summary>
public enum EReadoutState
{
    Unknown = 0,
    Empty,
    Live,
    Stale,
    Offline
}
