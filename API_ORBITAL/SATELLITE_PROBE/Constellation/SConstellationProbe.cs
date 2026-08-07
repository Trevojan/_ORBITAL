using SATELLITE_CORE.Constellation;
using SATELLITE_ENGINE.Satellite;

namespace SATELLITE_PROBE.Constellation;

/// <summary>Probes the core's orchestration — and that it borrows its rules rather than restating them.</summary>
public sealed class SConstellationProbe
{
    private readonly SConstellation _constellations = new(new SSatellite());

    private static MSatellite Member(int index, ESatelliteState state) =>
        MSatellite.Track($"PROBE-{index}", 20_000, 55).At(state);

    private static List<ISatellite> Members(int nominal, int lost) =>
    [
        .. Enumerable.Range(0, nominal).Select(i => Member(i, ESatelliteState.Nominal)),
        .. Enumerable.Range(nominal, lost).Select(i => Member(i, ESatelliteState.Lost))
    ];

    [Fact]
    public void Too_few_members_is_grounded_regardless_of_their_health()
    {
        var members = Members(nominal: StrConstellation.MinimumViableMembers - 1, lost: 0);

        Assert.Equal(EConstellationHealth.Grounded, _constellations.HealthOf(members));
    }

    [Fact]
    public void Three_of_four_nominal_meets_the_healthy_ratio() =>
        Assert.Equal(EConstellationHealth.Healthy, _constellations.HealthOf(Members(nominal: 3, lost: 1)));

    [Fact]
    public void Two_of_four_nominal_is_strained_but_not_failing() =>
        Assert.Equal(EConstellationHealth.Strained, _constellations.HealthOf(Members(nominal: 2, lost: 2)));

    [Fact]
    public void One_of_four_nominal_is_failing() =>
        Assert.Equal(EConstellationHealth.Failing, _constellations.HealthOf(Members(nominal: 1, lost: 3)));

    [Fact]
    public void Refresh_reclassifies_every_member_through_the_engine()
    {
        var constellation = MConstellation.Named("PROBE", [
            MSatellite.Track("A", 20_000, 55),
            MSatellite.Track("B", 20_000, 55),
            MSatellite.Track("C", 20_000, 55)]);

        var refreshed = _constellations.Refresh(constellation, TimeSpan.Zero);

        Assert.All(refreshed.Members, member => Assert.Equal(ESatelliteState.Nominal, member.State));
        Assert.Equal(EConstellationHealth.Healthy, refreshed.Health);
    }

    [Fact]
    public void A_stale_contact_gap_drives_the_whole_group_to_failing()
    {
        var constellation = MConstellation.Named("PROBE", [
            MSatellite.Track("A", 20_000, 55),
            MSatellite.Track("B", 20_000, 55),
            MSatellite.Track("C", 20_000, 55)]);

        var refreshed = _constellations.Refresh(constellation, StrSatellite.ContactGrace + TimeSpan.FromMinutes(1));

        Assert.All(refreshed.Members, member => Assert.Equal(ESatelliteState.Lost, member.State));
        Assert.Equal(EConstellationHealth.Failing, refreshed.Health);
    }

    [Fact]
    public void Adding_a_member_leaves_the_original_constellation_alone()
    {
        var original = MConstellation.Named("PROBE", [MSatellite.Track("A", 20_000, 55)]);

        var grown = original.With(MSatellite.Track("B", 20_000, 55));

        Assert.Equal(1, original.Count);
        Assert.Equal(2, grown.Count);
    }
}
