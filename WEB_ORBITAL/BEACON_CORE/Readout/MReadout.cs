namespace BEACON_CORE.Readout;

/// <summary>One reading as the page holds it, or the reason there is not one.</summary>
public sealed class MReadout
{
    private MReadout(
        string designator,
        double altitudeKm,
        string state,
        DateTimeOffset capturedAt,
        string detail,
        bool hasData)
    {
        Designator = designator;
        AltitudeKm = altitudeKm;
        State = state;
        CapturedAt = capturedAt;
        Detail = detail;
        HasData = hasData;
    }

    public string Designator { get; }

    public double AltitudeKm { get; }

    public string State { get; }

    public DateTimeOffset CapturedAt { get; }

    public string Detail { get; }

    public bool HasData { get; }

    /// <summary>Nothing has been asked for yet.</summary>
    public static MReadout Empty { get; } =
        new(StrReadout.NoDesignator, 0, string.Empty, DateTimeOffset.MinValue, StrReadout.NoDetail, false);

    public static MReadout From(string designator, double altitudeKm, string state, DateTimeOffset capturedAt)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(designator);

        return new MReadout(designator, altitudeKm, state, capturedAt, StrReadout.NoDetail, true);
    }

    /// <summary>The API could not be reached, and the page says so rather than showing nothing.</summary>
    public static MReadout Offline(string detail) =>
        new(StrReadout.NoDesignator, 0, string.Empty, DateTimeOffset.MinValue, detail, false);
}
