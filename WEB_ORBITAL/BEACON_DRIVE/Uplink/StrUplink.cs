namespace BEACON_DRIVE.Uplink;

/// <summary>Constants of the Uplink concept - every remote path spelled exactly once.</summary>
public readonly struct StrUplink
{
    public const string KeyHeader = "X-Orbital-Key";

    public const string ClassifyRoute = "api/satellites/classify";

    public const string HealthRoute = "api/health";

    public static readonly TimeSpan CallTimeout = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Empty on purpose: a browser app is served from an origin and calls back to it, so the
    /// host sets the base address at composition time. Hardcoding one here would be a CORS
    /// problem waiting for a deployment.
    /// </summary>
    public const string DefaultBaseAddress = "";
}
