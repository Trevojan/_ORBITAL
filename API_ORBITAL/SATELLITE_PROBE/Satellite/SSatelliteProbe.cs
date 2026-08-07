using SATELLITE_ENGINE.Satellite;

namespace SATELLITE_PROBE.Satellite;

/// <summary>Probes the engine's rules about what a satellite is.</summary>
public sealed class SSatelliteProbe
{
    private readonly SSatellite _satellites = new();

    private static MSatellite Orbit(double altitudeKm) => MSatellite.Track("PROBE-1", altitudeKm, 0);

    [Theory]
    [InlineData(100, ESatelliteRegime.Decaying)]
    [InlineData(StrSatellite.MinAltitudeKm, ESatelliteRegime.Low)]
    [InlineData(StrSatellite.LowOrbitCeilingKm, ESatelliteRegime.Low)]
    [InlineData(StrSatellite.LowOrbitCeilingKm + 1, ESatelliteRegime.Medium)]
    [InlineData(StrSatellite.MediumOrbitCeilingKm, ESatelliteRegime.Medium)]
    [InlineData(StrSatellite.MaxAltitudeKm, ESatelliteRegime.Geostationary)]
    public void RegimeOf_maps_each_altitude_band(double altitudeKm, ESatelliteRegime expected) =>
        Assert.Equal(expected, _satellites.RegimeOf(Orbit(altitudeKm)));

    [Theory]
    [InlineData(StrSatellite.MinAltitudeKm - 1, false)]
    [InlineData(StrSatellite.MinAltitudeKm, true)]
    [InlineData(StrSatellite.MaxAltitudeKm, true)]
    [InlineData(StrSatellite.MaxAltitudeKm + 1, false)]
    public void IsAltitudeSustainable_is_inclusive_at_both_edges(double altitudeKm, bool expected) =>
        Assert.Equal(expected, _satellites.IsAltitudeSustainable(altitudeKm));

    [Fact]
    public void Silence_beyond_the_grace_period_outranks_a_healthy_orbit()
    {
        var stale = StrSatellite.ContactGrace + TimeSpan.FromSeconds(1);

        Assert.Equal(ESatelliteState.Lost, _satellites.Classify(Orbit(20_000), stale));
    }

    [Fact]
    public void An_unsustainable_altitude_degrades_a_satellite_still_in_contact() =>
        Assert.Equal(ESatelliteState.Degraded, _satellites.Classify(Orbit(50), TimeSpan.Zero));

    [Fact]
    public void A_low_orbit_is_eclipsed_because_it_crosses_the_shadow_each_pass() =>
        Assert.Equal(ESatelliteState.Eclipsed, _satellites.Classify(Orbit(550), TimeSpan.Zero));

    [Fact]
    public void A_medium_orbit_in_contact_is_nominal() =>
        Assert.Equal(ESatelliteState.Nominal, _satellites.Classify(Orbit(20_000), TimeSpan.Zero));

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void A_satellite_cannot_be_built_without_a_designator(string designator) =>
        Assert.Throws<ArgumentException>(() => MSatellite.Track(designator, 500, 0));

    [Fact]
    public void A_negative_altitude_is_refused_at_construction() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => MSatellite.Track("PROBE-1", -1, 0));

    [Fact]
    public void Changing_state_yields_a_new_instance_and_leaves_the_original_untouched()
    {
        var original = Orbit(550);

        var moved = original.At(ESatelliteState.Lost);

        Assert.NotSame(original, moved);
        Assert.Equal(ESatelliteState.Unknown, original.State);
        Assert.Equal(ESatelliteState.Lost, moved.State);
    }
}
