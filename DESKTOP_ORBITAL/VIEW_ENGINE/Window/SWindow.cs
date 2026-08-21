namespace VIEW_ENGINE.Window;

/// <summary>
/// Every sizing rule the desktop app has. Pure: it never touches Photino,
/// which is exactly why it can be unit-tested without a display.
/// </summary>
public sealed class SWindow : IWindowOperations
{
    public bool IsUsable(ISized size)
    {
        ArgumentNullException.ThrowIfNull(size);

        return size.Width >= StrWindow.MinWidth && size.Height >= StrWindow.MinHeight;
    }

    /// <summary>Clamps a window up to the smallest size the UI still works at.</summary>
    public MWindow Normalize(MWindow window)
    {
        ArgumentNullException.ThrowIfNull(window);

        var bounds = new StrBounds(
            Math.Max(window.Width, StrWindow.MinWidth),
            Math.Max(window.Height, StrWindow.MinHeight));

        return window.Sized(bounds).At(StateFor(bounds));
    }

    public EWindowState StateFor(ISized size)
    {
        ArgumentNullException.ThrowIfNull(size);

        if (!IsUsable(size)) return EWindowState.Minimized;
        if (size.Width >= StrWindow.WideThreshold) return EWindowState.Wide;

        return EWindowState.Normal;
    }
}
