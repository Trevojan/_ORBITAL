namespace BEACON_ENGINE.Viewport;

/// <summary>
/// The layout band a width falls into, derived by <see cref="SViewport"/>.
///
/// Named for the shape of the space rather than for a device, because a desktop browser
/// dragged narrow is a handheld layout and a tablet held sideways is not.
/// </summary>
public enum EViewportClass
{
    Unknown = 0,
    Unusable,
    Handheld,
    Tablet,
    Desktop,
    Wide
}
