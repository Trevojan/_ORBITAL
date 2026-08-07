using RELAY_ENGINE.Envelope;

namespace RELAY_CORE.Channel;

/// <summary>
/// A queue between two nodes. Reuses <see cref="IAddressed"/> from the _ENGINE
/// instead of restating Origin and Destination.
/// </summary>
public interface IChannel : IAddressed
{
    EChannelState State { get; }

    int Depth { get; }
}

/// <summary>Orchestration contract: admit work, push it along, report back.</summary>
public interface IChannelOperations
{
    MChannel Admit(MChannel channel, MEnvelope envelope);

    MChannel Dispatch(MChannel channel);

    EChannelState StateOf(MChannel channel);
}
