using BEACON_ENGINE.Viewport;

namespace BEACON_PROBE.Viewport;

/// <summary>
/// Probes every layout rule with no browser attached — which is the whole reason the rules
/// live in an _ENGINE instead of in a component.
/// </summary>
public sealed class SViewportProbe
{
    private readonly SViewport _viewports = new();

    private static StrExtent Wide(int widthPx) => new(widthPx, 900);

    [Theory]
    [InlineData(StrViewport.MinUsableWidthPx - 1, EViewportClass.Unusable)]
    [InlineData(StrViewport.MinUsableWidthPx, EViewportClass.Handheld)]
    [InlineData(StrViewport.HandheldCeilingPx, EViewportClass.Handheld)]
    [InlineData(StrViewport.HandheldCeilingPx + 1, EViewportClass.Tablet)]
    [InlineData(StrViewport.TabletCeilingPx, EViewportClass.Tablet)]
    [InlineData(StrViewport.TabletCeilingPx + 1, EViewportClass.Desktop)]
    [InlineData(StrViewport.DesktopCeilingPx, EViewportClass.Desktop)]
    [InlineData(StrViewport.DesktopCeilingPx + 1, EViewportClass.Wide)]
    public void ClassOf_maps_each_band_inclusive_at_its_ceiling(int widthPx, EViewportClass expected) =>
        Assert.Equal(expected, _viewports.ClassOf(Wide(widthPx)));

    [Fact]
    public void A_width_below_the_usable_minimum_is_not_usable() =>
        Assert.False(_viewports.IsUsable(Wide(StrViewport.MinUsableWidthPx - 1)));

    [Fact]
    public void The_usable_minimum_itself_is_usable() =>
        Assert.True(_viewports.IsUsable(Wide(StrViewport.MinUsableWidthPx)));

    // n columns need n*column + (n-1)*gutter. With column 320 and gutter 24 that is
    // 320, 664 and 1008 for one, two and three -- so each pair below straddles a boundary.
    [Theory]
    [InlineData(0, 1)]
    [InlineData(320, 1)]
    [InlineData(663, 1)]
    [InlineData(664, 2)]
    [InlineData(1007, 2)]
    [InlineData(1008, 3)]
    public void ColumnsThatFit_counts_gutters_and_never_returns_zero(int widthPx, int expected) =>
        Assert.Equal(expected, _viewports.ColumnsThatFit(Wide(widthPx)));

    [Fact]
    public void A_viewport_too_narrow_to_hold_one_column_still_reports_one()
    {
        // One column that overflows is still the layout; no columns is not a layout.
        Assert.Equal(1, _viewports.ColumnsThatFit(Wide(1)));
    }

    [Fact]
    public void Normalize_clamps_a_narrow_viewport_up_to_the_usable_minimum()
    {
        var normalized = _viewports.Normalize(MViewport.Measured(100, 800));

        Assert.Equal(StrViewport.MinUsableWidthPx, normalized.WidthPx);
    }

    [Fact]
    public void Normalize_never_shrinks_a_viewport_that_already_fits()
    {
        var normalized = _viewports.Normalize(MViewport.Measured(1440, 900));

        Assert.Equal(1440, normalized.WidthPx);
    }

    [Fact]
    public void A_normalized_viewport_is_never_left_unusable()
    {
        var normalized = _viewports.Normalize(MViewport.Measured(1, 1));

        Assert.NotEqual(EViewportClass.Unusable, normalized.Class);
        Assert.True(_viewports.IsUsable(normalized));
    }

    [Fact]
    public void Height_does_not_affect_the_layout_class()
    {
        var tall = _viewports.ClassOf(new StrExtent(1200, 4000));
        var short_ = _viewports.ClassOf(new StrExtent(1200, 200));

        Assert.Equal(tall, short_);
    }

    [Fact]
    public void A_viewport_cannot_be_measured_with_a_negative_dimension() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => MViewport.Measured(-1, 800));

    [Fact]
    public void Resizing_yields_a_new_viewport_and_leaves_the_original_alone()
    {
        var original = MViewport.Measured(800, 600);

        var resized = original.Sized(new StrExtent(1600, 900));

        Assert.Equal(800, original.WidthPx);
        Assert.Equal(1600, resized.WidthPx);
    }

    [Theory]
    [InlineData(1200, 800, true)]
    [InlineData(800, 1200, false)]
    [InlineData(1000, 1000, true)]
    public void An_extent_reports_its_orientation(int widthPx, int heightPx, bool expected) =>
        Assert.Equal(expected, new StrExtent(widthPx, heightPx).IsLandscape);
}
