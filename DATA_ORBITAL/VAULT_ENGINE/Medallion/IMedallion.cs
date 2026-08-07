namespace VAULT_ENGINE.Medallion;

/// <summary>Anything that sits at a refinement tier.</summary>
public interface IRefinable
{
    EMedallionTier Tier { get; }
}

/// <summary>Anything whose origin can be audited.</summary>
public interface ITraceable
{
    string Source { get; }

    DateTimeOffset CapturedAt { get; }
}

/// <summary>
/// One record somewhere on its way from raw to reportable.
/// <c>IMedallion : IRefinable, ITraceable</c> — composed, not restated.
/// </summary>
public interface IMedallion : IRefinable, ITraceable
{
    IReadOnlyDictionary<string, string> Fields { get; }
}

/// <summary>Every rule about what may be promoted, and when.</summary>
public interface IMedallionOperations
{
    bool CanPromote(EMedallionTier from, EMedallionTier to);

    bool IsComplete(IMedallion record);

    bool IsMeasurable(IMedallion record);

    MMedallion Promote(MMedallion record);
}
