using System.Net;
using System.Net.Http.Json;

namespace BEACON_DRIVE.Uplink;

/// <summary>
/// The HTTP client. Transport concerns only - status codes, timeouts, deserialization.
/// It holds no opinion about what an altitude means; that belongs to the API.
/// </summary>
public sealed class SUplink(HttpClient client) : IUplinkOperations
{
    private readonly HttpClient _client = client ?? throw new ArgumentNullException(nameof(client));

    public async Task<MUplinkResult<RTrackedSatellite>> ClassifyAsync(
        RTrackSatellite request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            using var response = await _client.PostAsJsonAsync(
                StrUplink.ClassifyRoute, request, cancellationToken);

            if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            {
                return MUplinkResult<RTrackedSatellite>.Refused("The API rejected this key.");
            }

            if (!response.IsSuccessStatusCode)
            {
                return MUplinkResult<RTrackedSatellite>.Refused(
                    $"The API answered {(int)response.StatusCode}.");
            }

            var payload = await response.Content
                .ReadFromJsonAsync<RTrackedSatellite>(cancellationToken);

            if (payload is null)
            {
                return MUplinkResult<RTrackedSatellite>.Malformed("The API returned an empty body.");
            }

            return MUplinkResult<RTrackedSatellite>.Reached(payload);
        }
        catch (HttpRequestException failure)
        {
            // In a browser this is also what a blocked cross-origin request looks like:
            // the page is told the request failed, never why. Saying "unreachable" is the
            // honest amount of detail available on this side.
            return MUplinkResult<RTrackedSatellite>.Unreachable(failure.Message);
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return MUplinkResult<RTrackedSatellite>.Unreachable(
                $"No answer within {_client.Timeout.TotalSeconds:0}s.");
        }
        catch (System.Text.Json.JsonException failure)
        {
            return MUplinkResult<RTrackedSatellite>.Malformed(failure.Message);
        }
    }
}
