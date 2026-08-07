namespace VAULT_CORE.Ledger;

/// <summary>Constants of the Ledger concept.</summary>
public readonly struct StrLedger
{
    /// <summary>Records held in one batch before the ledger refuses more.</summary>
    public const int MaxBatch = 512;

    /// <summary>Bronze to gold is two rungs; the third pass proves the batch has stopped moving.</summary>
    public const int MaxPasses = 3;

    /// <summary>Above this share of quarantined records the source itself is suspect.</summary>
    public const double DegradedRatio = 0.25;
}
