namespace SATELLITE_ENGINE.Satellite;

/// <summary>
/// The Satellite as the app holds it. Immutable: state changes produce a new instance,
/// so no caller can mutate a model another caller is reading.
/// </summary>
public sealed class MSatellite : ISatellite
{
    private MSatellite(string designator, double altitudeKm, double inclinationDeg, ESatelliteState state)
    {
        Designator = designator;
        AltitudeKm = altitudeKm;
        InclinationDeg = inclinationDeg;
        State = state;
    }

    public string Designator { get; }

    public double AltitudeKm { get; }

    public double InclinationDeg { get; }

    public ESatelliteState State { get; }

    /// <summary>The only way in. A model cannot be constructed into an invalid shape.</summary>
    public static MSatellite Track(string designator, double altitudeKm, double inclinationDeg)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(designator);
        ArgumentOutOfRangeException.ThrowIfNegative(altitudeKm);

        return new MSatellite(designator, altitudeKm, inclinationDeg, ESatelliteState.Unknown);
    }

    /// <summary>Returns the same satellite carrying a newly derived state.</summary>
    public MSatellite At(ESatelliteState state) =>
        new(Designator, AltitudeKm, InclinationDeg, state);
}
