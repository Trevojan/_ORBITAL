using UPLINK_DRIVE.Uplink;
using VIEW_CORE.Readout;

namespace DESKTOP_PANEL.Shell;

/// <summary>
/// The join. This is the only type in DESKTOP_ORBITAL that sees both VIEW_CORE's
/// <see cref="IReadoutSource"/> and UPLINK_DRIVE's records — which is exactly why
/// the _CORE never has to know an API exists.
/// </summary>
public sealed class SShell(IUplinkOperations uplink) : IReadoutSource
{
    private readonly IUplinkOperations _uplink = uplink ?? throw new ArgumentNullException(nameof(uplink));

    public async Task<MReadout> LatestAsync(CancellationToken cancellationToken = default)
    {
        var request = new RTrackSatellite(
            StrShell.ProbeDesignator,
            StrShell.ProbeAltitudeKm,
            StrShell.ProbeInclinationDeg);

        var result = await _uplink.ClassifyAsync(request, cancellationToken);

        return result.Match(
            satellite => MReadout.From(
                satellite.Designator,
                satellite.AltitudeKm,
                satellite.State,
                DateTimeOffset.UtcNow),
            fault => MReadout.Offline($"{fault.Outcome}: {fault.Detail}"));
    }
}
