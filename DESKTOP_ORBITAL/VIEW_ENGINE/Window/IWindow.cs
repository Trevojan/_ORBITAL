namespace VIEW_ENGINE.Window;

/// <summary>Anything that occupies a rectangle.</summary>
public interface ISized
{
    int Width { get; }

    int Height { get; }
}

/// <summary>Anything the user sees a title bar for.</summary>
public interface IChromed
{
    string Title { get; }

    bool IsResizable { get; }
}

/// <summary>The window contract: <c>IWindow : ISized, IChromed</c>.</summary>
public interface IWindow : ISized, IChromed
{
    EWindowState State { get; }
}

/// <summary>Sizing rules, kept pure so they can be tested without opening a window.</summary>
public interface IWindowOperations
{
    bool IsUsable(ISized size);

    MWindow Normalize(MWindow window);

    EWindowState StateFor(ISized size);
}
