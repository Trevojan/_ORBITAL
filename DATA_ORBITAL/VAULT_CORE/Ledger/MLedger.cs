using VAULT_ENGINE.Medallion;

namespace VAULT_CORE.Ledger;

/// <summary>
/// A first-class collection of medallion records. The batch is never handed out raw,
/// so nothing can promote a record without going through <see cref="SLedger"/>.
/// </summary>
public sealed class MLedger : ILedger
{
    private readonly List<MMedallion> _records;

    private MLedger(string source, DateTimeOffset capturedAt, List<MMedallion> records, ELedgerState state)
    {
        Source = source;
        CapturedAt = capturedAt;
        _records = records;
        State = state;
    }

    public string Source { get; }

    public DateTimeOffset CapturedAt { get; }

    public ELedgerState State { get; }

    public int Depth => _records.Count;

    public bool HasRoom => _records.Count < StrLedger.MaxBatch;

    public IReadOnlyCollection<IMedallion> Records => _records.AsReadOnly();

    public static MLedger Opened(string source, DateTimeOffset capturedAt)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(source);

        return new MLedger(source, capturedAt, [], ELedgerState.Empty);
    }

    public MLedger Recording(MMedallion record)
    {
        ArgumentNullException.ThrowIfNull(record);

        return new MLedger(Source, CapturedAt, [.. _records, record], ELedgerState.Ingesting);
    }

    public MLedger Holding(IEnumerable<MMedallion> records, ELedgerState state) =>
        new(Source, CapturedAt, [.. records], state);

    public MLedger As(ELedgerState state) => new(Source, CapturedAt, _records, state);

    /// <summary>Projects every record through one refinement step, without exposing the batch.</summary>
    public IEnumerable<MMedallion> Project(Func<MMedallion, MMedallion> step)
    {
        ArgumentNullException.ThrowIfNull(step);

        return _records.Select(step);
    }

    public int CountAt(EMedallionTier tier) => _records.Count(record => record.Tier == tier);
}
