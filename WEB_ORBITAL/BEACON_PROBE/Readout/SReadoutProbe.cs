using BEACON_CORE.Readout;

namespace BEACON_PROBE.Readout;

/// <summary>Probes the page's presentation rules with no API and no page rendered.</summary>
public sealed class SReadoutProbe
{
    private readonly SReadout _readouts = new();

    private static readonly DateTimeOffset Now = new(2026, 8, 20, 12, 0, 0, TimeSpan.Zero);

    private static MReadout Captured(TimeSpan ago) =>
        MReadout.From("ORB-1", 550, "Eclipsed", Now - ago);

    [Fact]
    public void A_readout_nobody_has_filled_is_empty() =>
        Assert.Equal(EReadoutState.Empty, _readouts.StateOf(MReadout.Empty, Now));

    [Fact]
    public void A_readout_carrying_a_failure_is_offline_rather_than_empty() =>
        Assert.Equal(
            EReadoutState.Offline,
            _readouts.StateOf(MReadout.Offline("Unreachable: connection refused"), Now));

    [Fact]
    public void A_fresh_reading_is_live() =>
        Assert.Equal(EReadoutState.Live, _readouts.StateOf(Captured(TimeSpan.Zero), Now));

    [Fact]
    public void A_reading_at_the_staleness_edge_is_still_live() =>
        Assert.Equal(EReadoutState.Live, _readouts.StateOf(Captured(StrReadout.StaleAfter), Now));

    [Fact]
    public void A_reading_past_the_edge_is_stale() =>
        Assert.Equal(
            EReadoutState.Stale,
            _readouts.StateOf(Captured(StrReadout.StaleAfter + TimeSpan.FromSeconds(1)), Now));

    [Fact]
    public void A_failure_is_described_by_its_own_detail() =>
        Assert.Equal(
            "Unreachable: connection refused",
            _readouts.Describe(MReadout.Offline("Unreachable: connection refused")));

    [Fact]
    public void A_reading_is_described_as_designator_altitude_and_state() =>
        Assert.Equal("ORB-1 — 550 km — Eclipsed", _readouts.Describe(Captured(TimeSpan.Zero)));

    [Fact]
    public void An_empty_readout_says_so_instead_of_rendering_blanks() =>
        Assert.Equal("No reading yet.", _readouts.Describe(MReadout.Empty));

    [Fact]
    public void A_reading_cannot_be_built_without_a_designator() =>
        Assert.Throws<ArgumentException>(() => MReadout.From("  ", 550, "Nominal", Now));
}
