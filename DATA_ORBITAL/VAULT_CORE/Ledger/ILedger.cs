using VAULT_ENGINE.Medallion;

namespace VAULT_CORE.Ledger;

/// <summary>
/// A batch of records being refined together.
/// Reuses <see cref="ITraceable"/> from the _ENGINE: a ledger is auditable for the same reason a record is.
/// </summary>
public interface ILedger : ITraceable
{
    ELedgerState State { get; }

    int Depth { get; }
}

/// <summary>Orchestration contract: take records in, run passes, report what settled.</summary>
public interface ILedgerOperations
{
    MLedger Ingest(MLedger ledger, MMedallion record);

    MLedger Refine(MLedger ledger);

    MLedger Settle(MLedger ledger);

    StrRefinementTally TallyOf(MLedger ledger);
}
