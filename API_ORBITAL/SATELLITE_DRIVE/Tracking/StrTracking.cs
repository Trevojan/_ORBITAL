namespace SATELLITE_DRIVE.Tracking;

/// <summary>Route constants. Every path in the app is spelled exactly once, here.</summary>
public readonly struct StrTracking
{
    public const string GroupPrefix = "/satellites";

    public const string GroupTag = "Tracking";

    public const string ConstellationRoute = "/constellation/{designator}";

    public const string ClassifyRoute = "/classify";

    /// <summary>Contact silence assumed by a stateless classify call.</summary>
    public static readonly TimeSpan AssumedContactGap = TimeSpan.Zero;
}
