namespace VIEW_ENGINE.Window;

/// <summary>Constants of the Window concept.</summary>
public readonly struct StrWindow
{
    public const int MinWidth = 640;

    public const int MinHeight = 480;

    public const int DefaultWidth = 1280;

    public const int DefaultHeight = 800;

    /// <summary>Width at or above which the layout is treated as maximized.</summary>
    public const int WideThreshold = 1920;

    public const string DefaultTitle = "ORBITAL";
}

/// <summary>A lesser data group: a rectangle, with no window attached.</summary>
public readonly record struct StrBounds(int Width, int Height) : ISized
{
    public int Area => Width * Height;
}
