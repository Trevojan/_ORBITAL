using RELAY_ENGINE.Envelope;

namespace RELAY_CORE.Channel;

/// <summary>
/// Moves envelopes along without ever inspecting them. Every legality question is
/// delegated to <see cref="IEnvelopeOperations"/> — this service only decides *when* to ask.
/// </summary>
public sealed class SChannel(IEnvelopeOperations envelopes) : IChannelOperations
{
    private readonly IEnvelopeOperations _envelopes =
        envelopes ?? throw new ArgumentNullException(nameof(envelopes));

    /// <summary>Seals on the way in, so nothing untrusted is ever queued.</summary>
    public MChannel Admit(MChannel channel, MEnvelope envelope)
    {
        ArgumentNullException.ThrowIfNull(channel);
        ArgumentNullException.ThrowIfNull(envelope);

        if (!channel.HasRoom)
        {
            throw new InvalidOperationException($"Channel {channel.Origin}->{channel.Destination} is full.");
        }

        var admitted = channel.Admitting(_envelopes.Seal(envelope));

        return admitted.As(StateOf(admitted));
    }

    /// <summary>One pass: every envelope takes at most one legal step; delivered ones leave.</summary>
    public MChannel Dispatch(MChannel channel)
    {
        ArgumentNullException.ThrowIfNull(channel);

        var stepped = channel
            .Project(Step)
            .Where(envelope => envelope.Stage is not EEnvelopeStage.Delivered)
            .ToList();

        var dispatched = channel.Holding(stepped, EChannelState.Unknown);

        return dispatched.As(StateOf(dispatched));
    }

    public EChannelState StateOf(MChannel channel)
    {
        ArgumentNullException.ThrowIfNull(channel);

        if (channel.Depth is 0) return EChannelState.Idle;
        if (!channel.HasRoom) return EChannelState.Blocked;
        if (channel.Depth >= StrChannel.MaxDepth * StrChannel.SaturationRatio) return EChannelState.Saturated;

        return EChannelState.Flowing;
    }

    private MEnvelope Step(MEnvelope envelope)
    {
        var next = NextStage(envelope.Stage);

        if (!_envelopes.CanAdvance(envelope.Stage, next)) return envelope;

        return _envelopes.Advance(envelope, next);
    }

    private static EEnvelopeStage NextStage(EEnvelopeStage stage) => stage switch
    {
        EEnvelopeStage.Sealed => EEnvelopeStage.InTransit,
        EEnvelopeStage.InTransit => EEnvelopeStage.Delivered,
        _ => stage
    };
}
