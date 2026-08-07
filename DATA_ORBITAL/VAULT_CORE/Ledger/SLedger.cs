using VAULT_ENGINE.Medallion;

namespace VAULT_CORE.Ledger;

/// <summary>
/// Runs the Medallion ladder over a batch. It decides *when* to promote;
/// <see cref="IMedallionOperations"/> decides *whether* a record may be promoted.
/// </summary>
public sealed class SLedger(IMedallionOperations medallions) : ILedgerOperations
{
    private readonly IMedallionOperations _medallions =
        medallions ?? throw new ArgumentNullException(nameof(medallions));

    public MLedger Ingest(MLedger ledger, MMedallion record)
    {
        ArgumentNullException.ThrowIfNull(ledger);
        ArgumentNullException.ThrowIfNull(record);

        if (!ledger.HasRoom)
        {
            throw new InvalidOperationException($"Ledger '{ledger.Source}' is at its batch limit.");
        }

        return ledger.Recording(record);
    }

    /// <summary>One rung for every record that can still climb.</summary>
    public MLedger Refine(MLedger ledger)
    {
        ArgumentNullException.ThrowIfNull(ledger);

        return ledger.Holding(ledger.Project(_medallions.Promote), ELedgerState.Refining);
    }

    /// <summary>Passes until the batch stops changing, then reports the verdict.</summary>
    public MLedger Settle(MLedger ledger)
    {
        ArgumentNullException.ThrowIfNull(ledger);

        var current = ledger;

        for (var pass = 0; pass < StrLedger.MaxPasses; pass++)
        {
            var next = Refine(current);

            if (HasStopped(current, next)) return next.As(VerdictFor(next));

            current = next;
        }

        return current.As(VerdictFor(current));
    }

    public StrRefinementTally TallyOf(MLedger ledger)
    {
        ArgumentNullException.ThrowIfNull(ledger);

        return new StrRefinementTally(
            ledger.CountAt(EMedallionTier.Bronze),
            ledger.CountAt(EMedallionTier.Silver),
            ledger.CountAt(EMedallionTier.Gold),
            ledger.CountAt(EMedallionTier.Quarantined));
    }

    private static bool HasStopped(MLedger before, MLedger after) =>
        before.CountAt(EMedallionTier.Gold) == after.CountAt(EMedallionTier.Gold)
        && before.CountAt(EMedallionTier.Quarantined) == after.CountAt(EMedallionTier.Quarantined);

    private ELedgerState VerdictFor(MLedger ledger)
    {
        if (ledger.Depth is 0) return ELedgerState.Empty;

        var tally = TallyOf(ledger);

        if ((double)tally.Quarantined / tally.Total > StrLedger.DegradedRatio) return ELedgerState.Degraded;

        return ELedgerState.Settled;
    }
}
