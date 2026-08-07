using System.Text;
using RELAY_ENGINE.Envelope;

namespace RELAY_PROBE.Envelope;

/// <summary>Probes sealing, integrity, and the transition table.</summary>
public sealed class SEnvelopeProbe
{
    private readonly SEnvelope _envelopes = new();

    private static MEnvelope Draft(string body = "telemetry") =>
        MEnvelope.Draft("API_ORBITAL", "DATA_ORBITAL", Encoding.UTF8.GetBytes(body));

    [Fact]
    public void Sealing_stamps_a_checksum_and_moves_the_stage()
    {
        var sealedEnvelope = _envelopes.Seal(Draft());

        Assert.Equal(EEnvelopeStage.Sealed, sealedEnvelope.Stage);
        Assert.NotEqual(StrEnvelope.UnsealedChecksum, sealedEnvelope.Checksum);
    }

    [Fact]
    public void An_unsealed_envelope_is_never_intact() =>
        Assert.False(_envelopes.IsIntact(Draft()));

    [Fact]
    public void A_sealed_envelope_verifies_against_its_own_payload() =>
        Assert.True(_envelopes.IsIntact(_envelopes.Seal(Draft())));

    [Fact]
    public void Two_different_payloads_do_not_share_a_checksum()
    {
        var first = _envelopes.Seal(Draft("alpha"));
        var second = _envelopes.Seal(Draft("beta"));

        Assert.NotEqual(first.Checksum, second.Checksum);
    }

    [Theory]
    [InlineData(EEnvelopeStage.Draft, EEnvelopeStage.Sealed, true)]
    [InlineData(EEnvelopeStage.Sealed, EEnvelopeStage.InTransit, true)]
    [InlineData(EEnvelopeStage.InTransit, EEnvelopeStage.Delivered, true)]
    [InlineData(EEnvelopeStage.Draft, EEnvelopeStage.Dead, true)]
    [InlineData(EEnvelopeStage.InTransit, EEnvelopeStage.Dead, true)]
    [InlineData(EEnvelopeStage.Draft, EEnvelopeStage.InTransit, false)]
    [InlineData(EEnvelopeStage.Sealed, EEnvelopeStage.Delivered, false)]
    [InlineData(EEnvelopeStage.Delivered, EEnvelopeStage.Dead, false)]
    [InlineData(EEnvelopeStage.Delivered, EEnvelopeStage.InTransit, false)]
    public void The_transition_table_admits_only_forward_moves(
        EEnvelopeStage from,
        EEnvelopeStage to,
        bool expected) =>
        Assert.Equal(expected, _envelopes.CanAdvance(from, to));

    [Fact]
    public void Advancing_illegally_throws_rather_than_silently_doing_nothing() =>
        Assert.Throws<InvalidOperationException>(() =>
            _envelopes.Advance(Draft(), EEnvelopeStage.Delivered));

    [Fact]
    public void A_payload_beyond_the_relay_limit_is_refused_at_construction()
    {
        var oversized = new byte[StrEnvelope.MaxPayloadBytes + 1];

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            MEnvelope.Draft("API_ORBITAL", "DATA_ORBITAL", oversized));
    }

    [Fact]
    public void The_relay_never_needs_to_read_what_it_carries()
    {
        // The payload is bytes the engine has no schema for — that is the decoupling.
        var envelope = _envelopes.Seal(Draft("{\"anything\":true}"));

        Assert.True(_envelopes.IsIntact(envelope));
        Assert.Equal(EEnvelopeStage.Sealed, envelope.Stage);
    }
}
