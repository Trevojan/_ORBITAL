using VIEW_ENGINE.Window;

namespace VIEW_PROBE.Window;

/// <summary>
/// Probes the desktop sizing rules with no display attached —
/// which is the point of keeping them out of the host.
/// </summary>
public sealed class SWindowProbe
{
    private readonly SWindow _windows = new();

    [Theory]
    [InlineData(StrWindow.MinWidth, StrWindow.MinHeight, true)]
    [InlineData(StrWindow.MinWidth - 1, StrWindow.MinHeight, false)]
    [InlineData(StrWindow.MinWidth, StrWindow.MinHeight - 1, false)]
    [InlineData(StrWindow.DefaultWidth, StrWindow.DefaultHeight, true)]
    public void IsUsable_is_inclusive_at_the_minimum(int width, int height, bool expected) =>
        Assert.Equal(expected, _windows.IsUsable(new StrBounds(width, height)));

    [Theory]
    [InlineData(320, 240, StrWindow.MinWidth, StrWindow.MinHeight)]
    [InlineData(StrWindow.DefaultWidth, StrWindow.DefaultHeight, StrWindow.DefaultWidth, StrWindow.DefaultHeight)]
    public void Normalize_clamps_up_but_never_shrinks(
        int width,
        int height,
        int expectedWidth,
        int expectedHeight)
    {
        var normalized = _windows.Normalize(Window(width, height));

        Assert.Equal(expectedWidth, normalized.Width);
        Assert.Equal(expectedHeight, normalized.Height);
    }

    [Fact]
    public void A_normalized_window_is_never_left_unusable()
    {
        var normalized = _windows.Normalize(Window(1, 1));

        Assert.True(_windows.IsUsable(normalized));
        Assert.NotEqual(EWindowState.Minimized, normalized.State);
    }

    [Theory]
    [InlineData(320, 240, EWindowState.Minimized)]
    [InlineData(StrWindow.DefaultWidth, StrWindow.DefaultHeight, EWindowState.Normal)]
    [InlineData(StrWindow.WideThreshold, 1080, EWindowState.Wide)]
    public void StateFor_reads_the_layout_from_the_bounds(int width, int height, EWindowState expected) =>
        Assert.Equal(expected, _windows.StateFor(new StrBounds(width, height)));

    [Fact]
    public void A_window_cannot_be_built_without_a_title() =>
        Assert.Throws<ArgumentException>(() => MWindow.Named("  "));

    [Fact]
    public void Resizing_yields_a_new_window_and_leaves_the_original_alone()
    {
        var original = MWindow.Named(StrWindow.DefaultTitle);

        var resized = original.Sized(new StrBounds(1920, 1080));

        Assert.Equal(StrWindow.DefaultWidth, original.Width);
        Assert.Equal(1920, resized.Width);
    }

    private static MWindow Window(int width, int height) =>
        MWindow.Named(StrWindow.DefaultTitle).Sized(new StrBounds(width, height));
}
