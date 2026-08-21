namespace SATELLITE_ENGINE.Satellite;

/// <summary>
/// Every rule about what a satellite *is* lives here, and nowhere else.
/// Guard clauses only — no <c>else</c>, one level of indentation per method.
/// </summary>
public sealed class SSatellite : ISatelliteOperations
{
    public ESatelliteRegime RegimeOf(IOrbital orbit)
    {
        ArgumentNullException.ThrowIfNull(orbit);

        if (orbit.AltitudeKm < StrSatellite.MinAltitudeKm) return ESatelliteRegime.Decaying;
        if (orbit.AltitudeKm <= StrSatellite.LowOrbitCeilingKm) return ESatelliteRegime.Low;
        if (orbit.AltitudeKm <= StrSatellite.MediumOrbitCeilingKm) return ESatelliteRegime.Medium;

        return ESatelliteRegime.Geostationary;
    }

    public bool IsAltitudeSustainable(double altitudeKm) =>
        altitudeKm >= StrSatellite.MinAltitudeKm && altitudeKm <= StrSatellite.MaxAltitudeKm;

    public ESatelliteState Classify(IOrbital orbit, TimeSpan sinceLastContact)
    {
        ArgumentNullException.ThrowIfNull(orbit);

        if (sinceLastContact > StrSatellite.ContactGrace) return ESatelliteState.Lost;
        if (!IsAltitudeSustainable(orbit.AltitudeKm)) return ESatelliteState.Degraded;

        // A low orbit crosses Earth's shadow on every revolution.
        if (RegimeOf(orbit) is ESatelliteRegime.Low) return ESatelliteState.Eclipsed;

        return ESatelliteState.Nominal;
    }
}
