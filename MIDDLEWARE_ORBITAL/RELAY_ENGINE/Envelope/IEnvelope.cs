namespace RELAY_ENGINE.Envelope;

/// <summary>Anything that knows where it came from and where it is going.</summary>
public interface IAddressed
{
    string Origin { get; }

    string Destination { get; }
}

/// <summary>Anything whose integrity can be checked without opening it.</summary>
public interface ISealable
{
    string Checksum { get; }
}

/// <summary>
/// An opaque parcel in transit. The payload is bytes on purpose: the middleware
/// stays decoupled precisely because it cannot read what it carries.
/// </summary>
public interface IEnvelope : IAddressed, ISealable
{
    EEnvelopeStage Stage { get; }

    ReadOnlyMemory<byte> Payload { get; }
}

/// <summary>The rules of sealing, verifying, and moving an envelope along.</summary>
public interface IEnvelopeOperations
{
    MEnvelope Seal(MEnvelope envelope);

    bool IsIntact(IEnvelope envelope);

    bool CanAdvance(EEnvelopeStage from, EEnvelopeStage to);

    MEnvelope Advance(MEnvelope envelope, EEnvelopeStage stage);
}
