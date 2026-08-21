namespace SATELLITE_CORE.Constellation;

/// <summary>Constants of the Constellation concept.</summary>
public readonly struct StrConstellation
{
    /// <summary>A constellation below this many healthy members cannot guarantee coverage.</summary>
    public const int MinimumViableMembers = 3;

    /// <summary>Fraction of members that must be nominal before the group counts as healthy.</summary>
    public const double HealthyRatio = 0.75;

    /// <summary>Below this fraction the group is failing, not merely strained.</summary>
    public const double FailingRatio = 0.40;
}

/// <summary>A lesser data group: the tally a health verdict is computed from.</summary>
public readonly record struct StrCoverageTally(int Total, int Reachable)
{
    public double Ratio => Total is 0 ? 0d : (double)Reachable / Total;
}
