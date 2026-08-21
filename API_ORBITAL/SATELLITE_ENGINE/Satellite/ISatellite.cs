namespace SATELLITE_ENGINE.Satellite;

/// <summary>Anything the system can name and address.</summary>
public interface IIdentifiable
{
    string Designator { get; }
}

/// <summary>Anything that holds an orbit.</summary>
public interface IOrbital
{
    double AltitudeKm { get; }
    double InclinationDeg { get; }
}

/// <summary>
/// The Satellite contract, composed rather than duplicated:
/// <c>ISatellite : IIdentifiable, IOrbital</c>.
/// A consumer that only needs a name depends on <see cref="IIdentifiable"/> alone.
/// </summary>
public interface ISatellite : IIdentifiable, IOrbital
{
    ESatelliteState State { get; }
}

/// <summary>
/// The service contract. Split from <see cref="ISatellite"/> on purpose:
/// attributes and behaviour are separate responsibilities.
/// </summary>
public interface ISatelliteOperations
{
    ESatelliteRegime RegimeOf(IOrbital orbit);

    bool IsAltitudeSustainable(double altitudeKm);

    ESatelliteState Classify(IOrbital orbit, TimeSpan sinceLastContact);
}
