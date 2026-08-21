using VAULT_ENGINE.Medallion;

namespace VAULT_PROBE.Medallion;

/// <summary>Probes the Medallion ladder — one rung per pass, no path around validation.</summary>
public sealed class SMedallionProbe
{
    private readonly SMedallion _medallions = new();

    private static MMedallion Landed(params (string Key, string Value)[] fields) =>
        MMedallion.Landed("PROBE", DateTimeOffset.UnixEpoch, fields.ToDictionary(f => f.Key, f => f.Value));

    private static MMedallion Complete() => Landed(
        (StrMedallion.DesignatorField, "ORB-1"),
        (StrMedallion.AltitudeField, "550"));

    [Fact]
    public void Everything_enters_as_bronze() =>
        Assert.Equal(EMedallionTier.Bronze, Complete().Tier);

    [Fact]
    public void A_complete_bronze_record_reaches_silver() =>
        Assert.Equal(EMedallionTier.Silver, _medallions.Promote(Complete()).Tier);

    [Fact]
    public void A_bronze_record_missing_a_required_field_is_quarantined()
    {
        var incomplete = Landed((StrMedallion.DesignatorField, "ORB-1"));

        Assert.Equal(EMedallionTier.Quarantined, _medallions.Promote(incomplete).Tier);
    }

    [Fact]
    public void A_measurable_silver_record_reaches_gold()
    {
        var silver = _medallions.Promote(Complete());

        Assert.Equal(EMedallionTier.Gold, _medallions.Promote(silver).Tier);
    }

    [Fact]
    public void A_silver_record_whose_number_will_not_parse_is_quarantined()
    {
        var present_but_unparsable = Landed(
            (StrMedallion.DesignatorField, "ORB-1"),
            (StrMedallion.AltitudeField, "very high"));

        var silver = _medallions.Promote(present_but_unparsable);

        Assert.Equal(EMedallionTier.Silver, silver.Tier);
        Assert.Equal(EMedallionTier.Quarantined, _medallions.Promote(silver).Tier);
    }

    [Fact]
    public void Gold_is_the_top_of_the_ladder_and_stays_there()
    {
        var gold = _medallions.Promote(_medallions.Promote(Complete()));

        Assert.Equal(EMedallionTier.Gold, _medallions.Promote(gold).Tier);
    }

    [Theory]
    [InlineData(EMedallionTier.Bronze, EMedallionTier.Silver, true)]
    [InlineData(EMedallionTier.Silver, EMedallionTier.Gold, true)]
    [InlineData(EMedallionTier.Bronze, EMedallionTier.Gold, false)]
    [InlineData(EMedallionTier.Gold, EMedallionTier.Silver, false)]
    [InlineData(EMedallionTier.Quarantined, EMedallionTier.Silver, false)]
    [InlineData(EMedallionTier.Bronze, EMedallionTier.Quarantined, true)]
    [InlineData(EMedallionTier.Gold, EMedallionTier.Quarantined, false)]
    public void No_record_may_skip_a_rung(EMedallionTier from, EMedallionTier to, bool expected) =>
        Assert.Equal(expected, _medallions.CanPromote(from, to));

    [Fact]
    public void Field_names_are_matched_without_regard_to_case()
    {
        var shouting = Landed(
            (StrMedallion.DesignatorField.ToUpperInvariant(), "ORB-1"),
            (StrMedallion.AltitudeField.ToUpperInvariant(), "550"));

        Assert.True(_medallions.IsComplete(shouting));
    }

    [Fact]
    public void The_field_bag_is_copied_so_a_producer_cannot_mutate_an_accepted_record()
    {
        var source = new Dictionary<string, string>
        {
            [StrMedallion.DesignatorField] = "ORB-1",
            [StrMedallion.AltitudeField] = "550"
        };

        var record = MMedallion.Landed("PROBE", DateTimeOffset.UnixEpoch, source);
        source[StrMedallion.AltitudeField] = "tampered";

        Assert.Equal("550", record.Fields[StrMedallion.AltitudeField]);
    }
}
