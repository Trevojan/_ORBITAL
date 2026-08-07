namespace VAULT_ENGINE.Medallion;

/// <summary>
/// The Medallion tiers named in the README, made explicit.
/// Ordered deliberately so promotion is always "one step up".
/// </summary>
public enum EMedallionTier
{
    Unknown = 0,
    Bronze = 1,
    Silver = 2,
    Gold = 3,
    Quarantined = 9
}
