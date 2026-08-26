namespace BEACON_ENGINE.Viewport;

/// <summary>
/// Every layout rule the web application has. Pure: it never touches the browser, which is
/// exactly why BEACON_PROBE can check all of it with no page open.
///
/// Guard clauses, no <c>else</c>, one level of indentation per method (AR17).
/// </summary>
public sealed class SViewport : IViewportOperations
{
    public EViewportClass ClassOf(IMeasured measured)
    {
        ArgumentNullException.ThrowIfNull(measured);

        if (measured.WidthPx < StrViewport.MinUsableWidthPx) return EViewportClass.Unusable;
        if (measured.WidthPx <= StrViewport.HandheldCeilingPx) return EViewportClass.Handheld;
        if (measured.WidthPx <= StrViewport.TabletCeilingPx) return EViewportClass.Tablet;
        if (measured.WidthPx <= StrViewport.DesktopCeilingPx) return EViewportClass.Desktop;

        return EViewportClass.Wide;
    }

    public bool IsUsable(IMeasured measured)
    {
        ArgumentNullException.ThrowIfNull(measured);

        return measured.WidthPx >= StrViewport.MinUsableWidthPx;
    }

    /// <summary>
    /// How many readable columns fit side by side, gutters included.
    ///
    /// n columns need n*column + (n-1)*gutter pixels, so the largest n that fits is
    /// (width + gutter) / (column + gutter). Never fewer than one: a single column that
    /// overflows is still the layout, because the alternative is no layout at all.
    /// </summary>
    public int ColumnsThatFit(IMeasured measured)
    {
        ArgumentNullException.ThrowIfNull(measured);

        var fitted = (measured.WidthPx + StrViewport.GutterPx)
                     / (StrViewport.MinColumnWidthPx + StrViewport.GutterPx);

        return Math.Max(1, fitted);
    }

    /// <summary>Clamps a viewport up to the narrowest width the page still works at, then classifies it.</summary>
    public MViewport Normalize(MViewport viewport)
    {
        ArgumentNullException.ThrowIfNull(viewport);

        var extent = new StrExtent(
            Math.Max(viewport.WidthPx, StrViewport.MinUsableWidthPx),
            viewport.HeightPx);

        return viewport.Sized(extent).As(ClassOf(extent));
    }
}
