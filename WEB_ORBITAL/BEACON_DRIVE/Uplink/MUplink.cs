namespace BEACON_DRIVE.Uplink;

/// <summary>
/// The result of a call. A caller cannot reach the payload without also deciding what to
/// render when the API refused or was not there - see <see cref="Match{TResult}"/>.
/// </summary>
public sealed class MUplinkResult<TPayload> where TPayload : class
{
    private readonly TPayload? _payload;

    private MUplinkResult(EUplinkOutcome outcome, TPayload? payload, string detail)
    {
        Outcome = outcome;
        _payload = payload;
        Detail = detail;
    }

    public EUplinkOutcome Outcome { get; }

    public string Detail { get; }

    public bool WasReached => Outcome is EUplinkOutcome.Reached;

    public static MUplinkResult<TPayload> Reached(TPayload payload)
    {
        ArgumentNullException.ThrowIfNull(payload);

        return new MUplinkResult<TPayload>(EUplinkOutcome.Reached, payload, string.Empty);
    }

    public static MUplinkResult<TPayload> Refused(string detail) =>
        new(EUplinkOutcome.Refused, null, detail);

    public static MUplinkResult<TPayload> Unreachable(string detail) =>
        new(EUplinkOutcome.Unreachable, null, detail);

    public static MUplinkResult<TPayload> Malformed(string detail) =>
        new(EUplinkOutcome.Malformed, null, detail);

    /// <summary>The only way to read it - success and failure are both forced into the open.</summary>
    public TResult Match<TResult>(Func<TPayload, TResult> onReached, Func<RUplinkFault, TResult> onFault)
    {
        ArgumentNullException.ThrowIfNull(onReached);
        ArgumentNullException.ThrowIfNull(onFault);

        if (_payload is not null) return onReached(_payload);

        return onFault(new RUplinkFault(Outcome.ToString(), Detail));
    }
}
