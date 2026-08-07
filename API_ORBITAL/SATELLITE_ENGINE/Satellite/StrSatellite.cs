namespace SATELLITE_ENGINE.Satellite;

/// <summary>Constants of the Satellite concept. No behaviour, no state.</summary>
public readonly struct StrSatellite
{
    /// <summary>Below this, atmospheric drag decays the orbit within days.</summary>
    public const double MinAltitudeKm = 160.0;

    /// <summary>Geostationary altitude — the practical ceiling we track.</summary>
    public const double MaxAltitudeKm = 35_786.0;

    public const double LowOrbitCeilingKm = 2_000.0;

    public const double MediumOrbitCeilingKm = 35_000.0;

    /// <summary>Past this silence a satellite is no longer considered nominal.</summary>
    public static readonly TimeSpan ContactGrace = TimeSpan.FromMinutes(15);
}

/// <summary>A lesser data group: the span in which a ground station can reach a satellite.</summary>
public readonly record struct StrContactWindow(DateTimeOffset Opens, DateTimeOffset Closes)
{
    public TimeSpan Duration => Closes - Opens;

    public bool Covers(DateTimeOffset moment) => moment >= Opens && moment <= Closes;
}
