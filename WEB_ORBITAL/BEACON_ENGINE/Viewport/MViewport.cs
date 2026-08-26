namespace BEACON_ENGINE.Viewport;

/// <summary>
/// The viewport as the page holds it. Immutable (AR14): a resize produces a new instance,
/// so a component reading one cannot have it changed underneath by the component next to it.
/// </summary>
public sealed class MViewport : IViewport
{
    private MViewport(int widthPx, int heightPx, EViewportClass viewportClass)
    {
        WidthPx = widthPx;
        HeightPx = heightPx;
        Class = viewportClass;
    }

    public int WidthPx { get; }

    public int HeightPx { get; }

    public EViewportClass Class { get; }

    /// <summary>The only way in (AR15). A viewport cannot exist with a negative dimension.</summary>
    public static MViewport Measured(int widthPx, int heightPx)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(widthPx);
        ArgumentOutOfRangeException.ThrowIfNegative(heightPx);

        return new MViewport(widthPx, heightPx, EViewportClass.Unknown);
    }

    public MViewport Sized(StrExtent extent) =>
        new(extent.WidthPx, extent.HeightPx, Class);

    public MViewport As(EViewportClass viewportClass) =>
        new(WidthPx, HeightPx, viewportClass);
}
