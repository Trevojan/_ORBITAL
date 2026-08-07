namespace SATELLITE_DRIVE.Tracking;

/// <summary>
/// The outcome of an endpoint operation, before anyone decides what HTTP status it deserves.
/// Callers cannot read the payload without also handling the failure — see <see cref="Match{TResult}"/>.
/// </summary>
public sealed class MTrackingResult<TPayload> where TPayload : class
{
    private readonly TPayload? _payload;

    private MTrackingResult(ETrackingOutcome outcome, TPayload? payload, string detail)
    {
        Outcome = outcome;
        _payload = payload;
        Detail = detail;
    }

    public ETrackingOutcome Outcome { get; }

    public string Detail { get; }

    public static MTrackingResult<TPayload> Served(TPayload payload)
    {
        ArgumentNullException.ThrowIfNull(payload);

        return new MTrackingResult<TPayload>(ETrackingOutcome.Served, payload, string.Empty);
    }

    public static MTrackingResult<TPayload> Rejected(string detail) =>
        new(ETrackingOutcome.Rejected, null, detail);

    public static MTrackingResult<TPayload> NotTracked(string detail) =>
        new(ETrackingOutcome.NotTracked, null, detail);

    /// <summary>The only way to read the result — success and failure are both forced into the open.</summary>
    public TResult Match<TResult>(Func<TPayload, TResult> onServed, Func<RTrackingProblem, TResult> onProblem)
    {
        ArgumentNullException.ThrowIfNull(onServed);
        ArgumentNullException.ThrowIfNull(onProblem);

        if (_payload is not null) return onServed(_payload);

        return onProblem(new RTrackingProblem(Outcome.ToString(), Detail));
    }
}
