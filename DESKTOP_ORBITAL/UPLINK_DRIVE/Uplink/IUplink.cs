namespace UPLINK_DRIVE.Uplink;

/// <summary>
/// The outbound surface. Async, cancellable, and it never throws for a remote failure —
/// an unreachable API is an outcome, not an exception.
/// </summary>
public interface IUplinkOperations
{
    Task<MUplinkResult<RTrackedSatellite>> ClassifyAsync(
        RTrackSatellite request,
        CancellationToken cancellationToken = default);

    Task<MUplinkResult<RTrackedConstellation>> AssessAsync(
        string designator,
        IReadOnlyCollection<RTrackSatellite> members,
        CancellationToken cancellationToken = default);
}
