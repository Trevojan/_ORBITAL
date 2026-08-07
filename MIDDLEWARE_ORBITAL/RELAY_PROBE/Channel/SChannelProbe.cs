using System.Text;
using RELAY_CORE.Channel;
using RELAY_ENGINE.Envelope;

namespace RELAY_PROBE.Channel;

/// <summary>Probes the core's queue orchestration over the engine's rules.</summary>
public sealed class SChannelProbe
{
    private readonly SChannel _channels = new(new SEnvelope());

    private static MChannel Empty() => MChannel.Between("API_ORBITAL", "DATA_ORBITAL");

    private static MEnvelope Draft() =>
        MEnvelope.Draft("API_ORBITAL", "DATA_ORBITAL", Encoding.UTF8.GetBytes("telemetry"));

    [Fact]
    public void An_empty_channel_is_idle() =>
        Assert.Equal(EChannelState.Idle, _channels.StateOf(Empty()));

    [Fact]
    public void Admitting_seals_on_the_way_in_so_nothing_untrusted_is_queued()
    {
        var channel = _channels.Admit(Empty(), Draft());

        Assert.Equal(1, channel.Depth);
        Assert.Equal(1, channel.CountAt(EEnvelopeStage.Sealed));
        Assert.Equal(EChannelState.Flowing, channel.State);
    }

    [Fact]
    public void One_dispatch_pass_moves_a_sealed_envelope_into_transit()
    {
        var channel = _channels.Dispatch(_channels.Admit(Empty(), Draft()));

        Assert.Equal(1, channel.CountAt(EEnvelopeStage.InTransit));
    }

    [Fact]
    public void A_second_pass_delivers_the_envelope_and_drops_it_from_the_queue()
    {
        var channel = _channels.Dispatch(_channels.Dispatch(_channels.Admit(Empty(), Draft())));

        Assert.Equal(0, channel.Depth);
        Assert.Equal(EChannelState.Idle, channel.State);
    }

    [Fact]
    public void Admitting_leaves_the_original_channel_untouched()
    {
        var original = Empty();

        _channels.Admit(original, Draft());

        Assert.Equal(0, original.Depth);
    }

    [Fact]
    public void A_full_channel_refuses_new_work()
    {
        var channel = Empty();

        for (var i = 0; i < StrChannel.MaxDepth; i++)
        {
            channel = _channels.Admit(channel, Draft());
        }

        Assert.Equal(EChannelState.Blocked, channel.State);
        Assert.Throws<InvalidOperationException>(() => _channels.Admit(channel, Draft()));
    }
}
