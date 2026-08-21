namespace VAULT_ENGINE.Medallion;

/// <summary>
/// One record at one tier. Immutable, and the field bag is copied on the way in
/// so a producer cannot mutate what the vault has already accepted.
/// </summary>
public sealed class MMedallion : IMedallion
{
    private readonly Dictionary<string, string> _fields;

    private MMedallion(
        string source,
        DateTimeOffset capturedAt,
        Dictionary<string, string> fields,
        EMedallionTier tier)
    {
        Source = source;
        CapturedAt = capturedAt;
        _fields = fields;
        Tier = tier;
    }

    public string Source { get; }

    public DateTimeOffset CapturedAt { get; }

    public EMedallionTier Tier { get; }

    public IReadOnlyDictionary<string, string> Fields => _fields;

    /// <summary>Everything enters as bronze. No exceptions, no shortcuts.</summary>
    public static MMedallion Landed(string source, DateTimeOffset capturedAt, IDictionary<string, string> fields)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(source);
        ArgumentNullException.ThrowIfNull(fields);

        return new MMedallion(
            source,
            capturedAt,
            new Dictionary<string, string>(fields, StringComparer.OrdinalIgnoreCase),
            EMedallionTier.Bronze);
    }

    public MMedallion At(EMedallionTier tier) => new(Source, CapturedAt, _fields, tier);

    public bool Carries(string field) =>
        _fields.TryGetValue(field, out var value) && !string.IsNullOrWhiteSpace(value);

    public bool Reads(string field, out double value)
    {
        value = 0d;

        if (!_fields.TryGetValue(field, out var raw)) return false;

        return double.TryParse(raw, System.Globalization.CultureInfo.InvariantCulture, out value);
    }
}
