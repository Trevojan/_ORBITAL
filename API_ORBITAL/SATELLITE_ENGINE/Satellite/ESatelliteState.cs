namespace SATELLITE_ENGINE.Satellite;

/// <summary>Operational health, derived by <see cref="SSatellite"/> — never set by hand.</summary>
public enum ESatelliteState
{
    Unknown = 0,
    Nominal,
    Degraded,
    Eclipsed,
    Lost
}

/// <summary>Orbit band, derived from altitude alone.</summary>
public enum ESatelliteRegime
{
    Unknown = 0,
    Decaying,
    Low,
    Medium,
    Geostationary
}
