namespace VAULT_ENGINE.Medallion;

/// <summary>
/// The Medallion ladder. A record climbs one rung per pass, or it is quarantined —
/// there is no path that skips validation.
/// </summary>
public sealed class SMedallion : IMedallionOperations
{
    public bool CanPromote(EMedallionTier from, EMedallionTier to)
    {
        if (from is EMedallionTier.Quarantined) return false;
        if (to is EMedallionTier.Quarantined) return from is not EMedallionTier.Gold;

        return IsLadder(from) && IsLadder(to) && to - from == 1;
    }

    public bool IsComplete(IMedallion record)
    {
        ArgumentNullException.ThrowIfNull(record);

        return StrMedallion.RequiredFields.All(field =>
            record.Fields.TryGetValue(field, out var value) && !string.IsNullOrWhiteSpace(value));
    }

    public bool IsMeasurable(IMedallion record)
    {
        ArgumentNullException.ThrowIfNull(record);

        return StrMedallion.MeasurableFields.All(field =>
            record.Fields.TryGetValue(field, out var raw)
            && double.TryParse(raw, System.Globalization.CultureInfo.InvariantCulture, out _));
    }

    /// <summary>One rung. Bronze needs completeness; silver needs measurability.</summary>
    public MMedallion Promote(MMedallion record)
    {
        ArgumentNullException.ThrowIfNull(record);

        return record.Tier switch
        {
            EMedallionTier.Bronze when IsComplete(record) => record.At(EMedallionTier.Silver),
            EMedallionTier.Bronze => record.At(EMedallionTier.Quarantined),
            EMedallionTier.Silver when IsMeasurable(record) => record.At(EMedallionTier.Gold),
            EMedallionTier.Silver => record.At(EMedallionTier.Quarantined),
            _ => record
        };
    }

    private static bool IsLadder(EMedallionTier tier) =>
        tier is EMedallionTier.Bronze or EMedallionTier.Silver or EMedallionTier.Gold;
}
