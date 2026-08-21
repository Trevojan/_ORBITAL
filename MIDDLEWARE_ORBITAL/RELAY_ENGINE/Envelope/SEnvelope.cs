using System.Security.Cryptography;

namespace RELAY_ENGINE.Envelope;

/// <summary>
/// Sealing, integrity, and the legal stage transitions. This is the whole of what
/// MIDDLEWARE_ORBITAL knows how to do — note that none of it mentions a satellite.
/// </summary>
public sealed class SEnvelope : IEnvelopeOperations
{
    public MEnvelope Seal(MEnvelope envelope)
    {
        ArgumentNullException.ThrowIfNull(envelope);

        return envelope.SealedWith(ChecksumOf(envelope.Payload));
    }

    public bool IsIntact(IEnvelope envelope)
    {
        ArgumentNullException.ThrowIfNull(envelope);

        if (envelope.Checksum == StrEnvelope.UnsealedChecksum) return false;

        return envelope.Checksum == ChecksumOf(envelope.Payload);
    }

    /// <summary>The transition table. Every legal move in the relay is on this one screen.</summary>
    public bool CanAdvance(EEnvelopeStage from, EEnvelopeStage to) => (from, to) switch
    {
        (EEnvelopeStage.Draft, EEnvelopeStage.Sealed) => true,
        (EEnvelopeStage.Sealed, EEnvelopeStage.InTransit) => true,
        (EEnvelopeStage.InTransit, EEnvelopeStage.Delivered) => true,
        (_, EEnvelopeStage.Dead) => from is not EEnvelopeStage.Delivered,
        _ => false
    };

    public MEnvelope Advance(MEnvelope envelope, EEnvelopeStage stage)
    {
        ArgumentNullException.ThrowIfNull(envelope);

        if (!CanAdvance(envelope.Stage, stage))
        {
            throw new InvalidOperationException($"An envelope cannot move from {envelope.Stage} to {stage}.");
        }

        return envelope.At(stage);
    }

    private static string ChecksumOf(ReadOnlyMemory<byte> payload) =>
        Convert.ToHexStringLower(SHA256.HashData(payload.Span));
}
