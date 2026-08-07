namespace RELAY_ENGINE.Envelope;

/// <summary>An envelope as the relay holds it. Immutable — every stage change is a new instance.</summary>
public sealed class MEnvelope : IEnvelope
{
    private MEnvelope(
        string origin,
        string destination,
        ReadOnlyMemory<byte> payload,
        EEnvelopeStage stage,
        string checksum)
    {
        Origin = origin;
        Destination = destination;
        Payload = payload;
        Stage = stage;
        Checksum = checksum;
    }

    public string Origin { get; }

    public string Destination { get; }

    public ReadOnlyMemory<byte> Payload { get; }

    public EEnvelopeStage Stage { get; }

    public string Checksum { get; }

    public static MEnvelope Draft(string origin, string destination, ReadOnlyMemory<byte> payload)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(origin);
        ArgumentException.ThrowIfNullOrWhiteSpace(destination);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(payload.Length, StrEnvelope.MaxPayloadBytes);

        return new MEnvelope(origin, destination, payload, EEnvelopeStage.Draft, StrEnvelope.UnsealedChecksum);
    }

    public MEnvelope SealedWith(string checksum) =>
        new(Origin, Destination, Payload, EEnvelopeStage.Sealed, checksum);

    public MEnvelope At(EEnvelopeStage stage) =>
        new(Origin, Destination, Payload, stage, Checksum);
}
