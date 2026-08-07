namespace VIEW_CORE.Readout;

/// <summary>
/// What the view needs, declared by the view. UPLINK_DRIVE is never named here:
/// DESKTOP_PANEL implements this contract by adapting the driver, so the _CORE
/// depends on an abstraction it owns rather than on the outward ring.
/// </summary>
public interface IReadoutSource
{
    Task<MReadout> LatestAsync(CancellationToken cancellationToken = default);
}

/// <summary>How a readout should be presented, kept out of the markup.</summary>
public interface IReadoutOperations
{
    EReadoutState StateOf(MReadout readout, DateTimeOffset now);

    string Describe(MReadout readout);
}
