namespace BEACON_ENGINE.Viewport;

/// <summary>
/// Constants of the Viewport concept. Every breakpoint in the application is spelled
/// exactly once, here, so a stylesheet and a layout rule cannot disagree about where
/// the boundary is.
/// </summary>
public readonly struct StrViewport
{
    /// <summary>Below this the page cannot be laid out at all, only scrolled.</summary>
    public const int MinUsableWidthPx = 320;

    public const int HandheldCeilingPx = 640;

    public const int TabletCeilingPx = 1024;

    public const int DesktopCeilingPx = 1536;

    /// <summary>Narrower than this, a column stops being readable and becomes a list of fragments.</summary>
    public const int MinColumnWidthPx = 320;

    public const int GutterPx = 24;
}

/// <summary>A lesser data group: a measured rectangle, with no page attached.</summary>
public readonly record struct StrExtent(int WidthPx, int HeightPx) : IMeasured, ISpaced
{
    public bool IsLandscape => WidthPx >= HeightPx;
}
