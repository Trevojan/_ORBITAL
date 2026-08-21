namespace UPLINK_DRIVE.Uplink;

/// <summary>Constants of the Uplink concept — every remote path spelled exactly once.</summary>
public readonly struct StrUplink
{
    public const string KeyHeader = "X-Orbital-Key";

    public const string ClassifyRoute = "api/satellites/classify";

    public const string ConstellationRoute = "api/satellites/constellation";

    public const string HealthRoute = "api/health";

    public static readonly TimeSpan CallTimeout = TimeSpan.FromSeconds(10);

    /// <summary>Where the desktop looks for the API when nothing is configured.</summary>
    public static readonly Uri DefaultBaseAddress = new("http://localhost:5199");
}
