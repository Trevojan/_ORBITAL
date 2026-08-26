namespace BEACON_ENGINE.Viewport;

/// <summary>Anything with a measurable width.</summary>
public interface IMeasured
{
    int WidthPx { get; }
}

/// <summary>Anything that reports how much vertical room it has.</summary>
public interface ISpaced
{
    int HeightPx { get; }
}

/// <summary>
/// The area the page has been given. Composed rather than restated:
/// <c>IViewport : IMeasured, ISpaced</c> (AR13). A rule that only cares about
/// width depends on <see cref="IMeasured"/> alone.
/// </summary>
public interface IViewport : IMeasured, ISpaced
{
    EViewportClass Class { get; }
}

/// <summary>
/// Layout rules. Split from <see cref="IViewport"/> because attributes and behaviour are
/// separate responsibilities, and because these are the parts worth proving.
/// </summary>
public interface IViewportOperations
{
    EViewportClass ClassOf(IMeasured measured);

    bool IsUsable(IMeasured measured);

    int ColumnsThatFit(IMeasured measured);

    MViewport Normalize(MViewport viewport);
}
