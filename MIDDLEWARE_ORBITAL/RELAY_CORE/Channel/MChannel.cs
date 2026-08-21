using RELAY_ENGINE.Envelope;

namespace RELAY_CORE.Channel;

/// <summary>
/// A first-class collection of in-flight envelopes. The queue never leaves this class,
/// so no caller can reorder or drain it behind the service's back.
/// </summary>
public sealed class MChannel : IChannel
{
    private readonly List<MEnvelope> _queue;

    private MChannel(string origin, string destination, List<MEnvelope> queue, EChannelState state)
    {
        Origin = origin;
        Destination = destination;
        _queue = queue;
        State = state;
    }

    public string Origin { get; }

    public string Destination { get; }

    public EChannelState State { get; }

    public int Depth => _queue.Count;

    public bool HasRoom => _queue.Count < StrChannel.MaxDepth;

    public static MChannel Between(string origin, string destination)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(origin);
        ArgumentException.ThrowIfNullOrWhiteSpace(destination);

        return new MChannel(origin, destination, [], EChannelState.Idle);
    }

    public MChannel Admitting(MEnvelope envelope)
    {
        ArgumentNullException.ThrowIfNull(envelope);

        return new MChannel(Origin, Destination, [.. _queue, envelope], State);
    }

    public MChannel Holding(IEnumerable<MEnvelope> queue, EChannelState state) =>
        new(Origin, Destination, [.. queue], state);

    public MChannel As(EChannelState state) => new(Origin, Destination, _queue, state);

    /// <summary>Projects every held envelope without handing out the queue.</summary>
    public IEnumerable<MEnvelope> Project(Func<MEnvelope, MEnvelope> step)
    {
        ArgumentNullException.ThrowIfNull(step);

        return _queue.Select(step);
    }

    public int CountAt(EEnvelopeStage stage) => _queue.Count(envelope => envelope.Stage == stage);
}
