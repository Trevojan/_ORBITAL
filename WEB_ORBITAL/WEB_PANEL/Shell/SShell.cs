using BEACON_CORE.Readout;
using BEACON_DRIVE.Uplink;

namespace WEB_PANEL.Shell;

/// <summary>
/// The join. The only type in WEB_ORBITAL that sees both BEACON_CORE's
/// <see cref="IReadoutSource"/> and BEACON_DRIVE's records — which is precisely why the
/// _CORE never has to know that an API exists (AR7).
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
