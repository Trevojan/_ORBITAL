namespace RELAY_ENGINE.Envelope;

/// <summary>Constants of the Envelope concept.</summary>
public readonly struct StrEnvelope
{
    /// <summary>Anything larger belongs in DATA_ORBITAL with a pointer relayed instead.</summary>
    public const int MaxPayloadBytes = 256 * 1024;

    public const string UnsealedChecksum = "";

    public static readonly TimeSpan DeliveryDeadline = TimeSpan.FromSeconds(30);
}
