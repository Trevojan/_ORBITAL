namespace RELAY_ENGINE.Envelope;

/// <summary>
/// Where an envelope sits in its journey. Legal transitions are enforced by
/// <see cref="SEnvelope.CanAdvance"/> — the enum itself promises nothing.
/// </summary>
public enum EEnvelopeStage
{
    Unknown = 0,
    Draft,
    Sealed,
    InTransit,
    Delivered,
    Dead
}
