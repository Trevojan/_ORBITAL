namespace RELAY_CORE.Channel;

/// <summary>Constants of the Channel concept.</summary>
public readonly struct StrChannel
{
    /// <summary>Envelopes held before the channel refuses new work.</summary>
    public const int MaxDepth = 128;

    /// <summary>Fraction of <see cref="MaxDepth"/> at which the channel counts as saturated.</summary>
    public const double SaturationRatio = 0.80;
}
