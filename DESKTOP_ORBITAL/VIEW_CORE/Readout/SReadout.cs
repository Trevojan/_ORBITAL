namespace VIEW_CORE.Readout;

/// <summary>Presentation rules for a reading. Pure, so VIEW_PROBE can check them with no window open.</summary>
public sealed class SReadout : IReadoutOperations
{
    public EReadoutState StateOf(MReadout readout, DateTimeOffset now)
    {
        ArgumentNullException.ThrowIfNull(readout);

        if (!readout.HasData && readout.Detail.Length > 0) return EReadoutState.Offline;
        if (!readout.HasData) return EReadoutState.Empty;
        if (now - readout.CapturedAt > StrReadout.StaleAfter) return EReadoutState.Stale;

        return EReadoutState.Live;
    }

    public string Describe(MReadout readout)
    {
        ArgumentNullException.ThrowIfNull(readout);

        if (!readout.HasData && readout.Detail.Length > 0) return readout.Detail;
        if (!readout.HasData) return "No reading yet.";

        return $"{readout.Designator} — {readout.AltitudeKm:0} km — {readout.State}";
    }
}
