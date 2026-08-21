using SATELLITE_ENGINE.Satellite;

namespace SATELLITE_CORE.Constellation;

/// <summary>
/// A named group of tracked satellites. Reuses <see cref="IIdentifiable"/> from the
/// _ENGINE rather than restating a Designator property — split, never duplicate.
/// </summary>
public interface IConstellation : IIdentifiable
{
    IReadOnlyCollection<ISatellite> Members { get; }

    EConstellationHealth Health { get; }
}

/// <summary>Orchestration contract. The _CORE coordinates models; it does not re-derive rules.</summary>
public interface IConstellationOperations
{
    MConstellation Refresh(MConstellation constellation, TimeSpan sinceLastContact);

    EConstellationHealth HealthOf(IReadOnlyCollection<ISatellite> members);
}
