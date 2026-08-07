namespace VIEW_ENGINE.Window;

/// <summary>The window as the app holds it, before any OS handle exists.</summary>
public sealed class MWindow : IWindow
{
    private MWindow(string title, int width, int height, bool isResizable, EWindowState state)
    {
        Title = title;
        Width = width;
        Height = height;
        IsResizable = isResizable;
        State = state;
    }

    public string Title { get; }

    public int Width { get; }

    public int Height { get; }

    public bool IsResizable { get; }

    public EWindowState State { get; }

    public static MWindow Named(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        return new MWindow(
            title,
            StrWindow.DefaultWidth,
            StrWindow.DefaultHeight,
            isResizable: true,
            EWindowState.Hidden);
    }

    public MWindow Sized(StrBounds bounds) =>
        new(Title, bounds.Width, bounds.Height, IsResizable, State);

    public MWindow At(EWindowState state) =>
        new(Title, Width, Height, IsResizable, state);
}
