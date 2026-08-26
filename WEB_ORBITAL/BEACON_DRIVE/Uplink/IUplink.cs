namespace BEACON_DRIVE.Uplink;

/// <summary>
/// The outbound surface. Async, cancellable, and it never throws for a remote failure -
/// an unreachable API is an outcome the page renders, not an exception it catches.
/// </summary>
public interface IUplinkOperations
{
    Task<MUplinkResult<RTrackedSatellite>> ClassifyAsync(
        RTrackSatellite request,
        CancellationToken cancellationToken = default);
}
