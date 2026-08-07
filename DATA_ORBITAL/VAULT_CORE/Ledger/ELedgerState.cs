namespace VAULT_CORE.Ledger;

/// <summary>Where a batch stands, derived by <see cref="SLedger"/>.</summary>
public enum ELedgerState
{
    Unknown = 0,
    Empty,
    Ingesting,
    Refining,
    Settled,
    Degraded
}
