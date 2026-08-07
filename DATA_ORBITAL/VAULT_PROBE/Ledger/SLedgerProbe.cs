using VAULT_CORE.Ledger;
using VAULT_ENGINE.Medallion;

namespace VAULT_PROBE.Ledger;

/// <summary>Probes the batch orchestration that drives the ladder to a standstill.</summary>
public sealed class SLedgerProbe
{
    private readonly SLedger _ledgers = new(new SMedallion());

    private static MMedallion Complete(int index) => MMedallion.Landed(
        "PROBE",
        DateTimeOffset.UnixEpoch,
        new Dictionary<string, string>
        {
            [StrMedallion.DesignatorField] = $"ORB-{index}",
            [StrMedallion.AltitudeField] = "550"
        });

    private static MMedallion Incomplete(int index) => MMedallion.Landed(
        "PROBE",
        DateTimeOffset.UnixEpoch,
        new Dictionary<string, string> { [StrMedallion.DesignatorField] = $"ORB-{index}" });

    private MLedger Batch(int complete, int incomplete)
    {
        var ledger = MLedger.Opened("PROBE", DateTimeOffset.UnixEpoch);

        for (var i = 0; i < complete; i++) ledger = _ledgers.Ingest(ledger, Complete(i));
        for (var i = 0; i < incomplete; i++) ledger = _ledgers.Ingest(ledger, Incomplete(complete + i));

        return ledger;
    }

    [Fact]
    public void An_empty_ledger_settles_as_empty() =>
        Assert.Equal(
            ELedgerState.Empty,
            _ledgers.Settle(MLedger.Opened("PROBE", DateTimeOffset.UnixEpoch)).State);

    [Fact]
    public void Settling_carries_complete_records_all_the_way_to_gold()
    {
        // Bronze to gold is two rungs; a single refinement pass would stop at silver.
        var settled = _ledgers.Settle(Batch(complete: 4, incomplete: 0));

        Assert.Equal(new StrRefinementTally(0, 0, 4, 0), _ledgers.TallyOf(settled));
        Assert.Equal(ELedgerState.Settled, settled.State);
    }

    [Fact]
    public void One_refinement_pass_only_reaches_silver()
    {
        var refined = _ledgers.Refine(Batch(complete: 4, incomplete: 0));

        Assert.Equal(new StrRefinementTally(0, 4, 0, 0), _ledgers.TallyOf(refined));
    }

    [Fact]
    public void Incomplete_records_are_quarantined_rather_than_promoted()
    {
        var settled = _ledgers.Settle(Batch(complete: 0, incomplete: 3));

        Assert.Equal(3, _ledgers.TallyOf(settled).Quarantined);
    }

    [Fact]
    public void A_batch_with_too_much_quarantine_marks_the_source_itself_suspect()
    {
        // 2 of 4 quarantined is 50%, past the 25% degraded threshold.
        var settled = _ledgers.Settle(Batch(complete: 2, incomplete: 2));

        Assert.Equal(ELedgerState.Degraded, settled.State);
    }

    [Fact]
    public void A_batch_within_the_quarantine_tolerance_still_settles()
    {
        // 1 of 8 quarantined is 12.5%, inside tolerance.
        var settled = _ledgers.Settle(Batch(complete: 7, incomplete: 1));

        Assert.Equal(ELedgerState.Settled, settled.State);
        Assert.Equal(7, _ledgers.TallyOf(settled).Gold);
    }

    [Fact]
    public void Ingesting_leaves_the_original_batch_alone()
    {
        var original = MLedger.Opened("PROBE", DateTimeOffset.UnixEpoch);

        _ledgers.Ingest(original, Complete(0));

        Assert.Equal(0, original.Depth);
    }
}
