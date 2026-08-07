using SATELLITE_ENGINE.Satellite;

namespace SATELLITE_CORE.Constellation;

/// <summary>
/// Orchestration only. Notice what is absent: no altitude thresholds, no state rules.
/// Those belong to <see cref="ISatelliteOperations"/> in the _ENGINE, and this service asks for them.
/// </summary>
public sealed class SConstellation(ISatelliteOperations satellites) : IConstellationOperations
{
    private readonly ISatelliteOperations _satellites =
        satellites ?? throw new ArgumentNullException(nameof(satellites));

    /// <summary>Reclassifies every member through the engine, then scores the group.</summary>
    public MConstellation Refresh(MConstellation constellation, TimeSpan sinceLastContact)
    {
        ArgumentNullException.ThrowIfNull(constellation);

        var reclassified = constellation
            .Project(member => member.At(_satellites.Classify(member, sinceLastContact)))
            .ToList();

        return constellation.Reporting(reclassified, HealthOf(reclassified));
    }

    public EConstellationHealth HealthOf(IReadOnlyCollection<ISatellite> members)
    {
        ArgumentNullException.ThrowIfNull(members);

        if (members.Count < StrConstellation.MinimumViableMembers) return EConstellationHealth.Grounded;

        var tally = new StrCoverageTally(
            members.Count,
            members.Count(member => member.State is ESatelliteState.Nominal));

        if (tally.Ratio >= StrConstellation.HealthyRatio) return EConstellationHealth.Healthy;
        if (tally.Ratio >= StrConstellation.FailingRatio) return EConstellationHealth.Strained;

        return EConstellationHealth.Failing;
    }
}
