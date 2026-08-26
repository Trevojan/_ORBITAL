namespace BEACON_CORE.Readout;

/// <summary>Constants of the Readout concept.</summary>
public readonly struct StrReadout
{
    /// <summary>Past this age a reading is shown as stale rather than live.</summary>
    public static readonly TimeSpan StaleAfter = TimeSpan.FromSeconds(30);

    public const string NoDesignator = "—";

    public const string NoDetail = "";
}
