using SATELLITE_ENGINE.Satellite;

namespace SATELLITE_CORE.Constellation;

/// <summary>
/// A first-class collection: the member list is never handed out raw, and every
/// operation over it lives on this type instead of leaking into callers.
/// </summary>
public sealed class MConstellation : IConstellation
{
    private readonly List<MSatellite> _members;

    private MConstellation(string designator, List<MSatellite> members, EConstellationHealth health)
    {
        Designator = designator;
        _members = members;
        Health = health;
    }

    public string Designator { get; }

    public EConstellationHealth Health { get; }

    public IReadOnlyCollection<ISatellite> Members => _members.AsReadOnly();

    public int Count => _members.Count;

    public static MConstellation Named(string designator, IEnumerable<MSatellite> members)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(designator);
        ArgumentNullException.ThrowIfNull(members);

        return new MConstellation(designator, [.. members], EConstellationHealth.Unknown);
    }

    public MConstellation With(MSatellite satellite)
    {
        ArgumentNullException.ThrowIfNull(satellite);

        return new MConstellation(Designator, [.. _members, satellite], EConstellationHealth.Unknown);
    }

    /// <summary>Rebuilds the group from freshly classified members plus the resulting verdict.</summary>
    public MConstellation Reporting(IEnumerable<MSatellite> members, EConstellationHealth health) =>
        new(Designator, [.. members], health);

    /// <summary>Projects each member through <paramref name="reclassify"/> without exposing the list.</summary>
    public IEnumerable<MSatellite> Project(Func<MSatellite, MSatellite> reclassify)
    {
        ArgumentNullException.ThrowIfNull(reclassify);

        return _members.Select(reclassify);
    }

    public StrCoverageTally TallyReachable() =>
        new(_members.Count, _members.Count(member => member.State is ESatelliteState.Nominal));
}
