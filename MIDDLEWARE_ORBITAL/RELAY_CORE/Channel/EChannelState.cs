namespace RELAY_CORE.Channel;

/// <summary>How much room a channel has left, derived by <see cref="SChannel"/>.</summary>
public enum EChannelState
{
    Unknown = 0,
    Idle,
    Flowing,
    Saturated,
    Blocked
}
