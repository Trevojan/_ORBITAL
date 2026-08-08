using System.Net;
using System.Net.Http.Json;

namespace UPLINK_DRIVE.Uplink;

/// <summary>
/// The HTTP client. It owns transport concerns only — status codes, timeouts, and
/// deserialization. It holds no opinion about what an altitude means.
/// </summary>
public sealed class SUplink(HttpClient client) : IUplinkOperations
{
    private readonly HttpClient _client = client ?? throw new ArgumentNullException(nameof(client));

    public Task<MUplinkResult<RTrackedSatellite>> ClassifyAsync(
        RTrackSatellite request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return SendAsync<RTrackSatellite, RTrackedSatellite>(
            StrUplink.ClassifyRoute,
            request,
            cancellationToken);
    }

    public Task<MUplinkResult<RTrackedConstellation>> AssessAsync(
        string designator,
        IReadOnlyCollection<RTrackSatellite> members,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(designator);
        ArgumentNullException.ThrowIfNull(members);

        return SendAsync<IReadOnlyCollection<RTrackSatellite>, RTrackedConstellation>(
            $"{StrUplink.ConstellationRoute}/{Uri.EscapeDataString(designator)}",
            members,
            cancellationToken);
    }

    /// <summary>
    /// One place where a remote failure becomes an outcome. Every transport concern
    /// is caught here so no caller ever has to handle an HttpRequestException.
    /// </summary>
    private async Task<MUplinkResult<TResponse>> SendAsync<TRequest, TResponse>(
        string route,
        TRequest body,
        CancellationToken cancellationToken)
        where TResponse : class
    {
        try
        {
            using var response = await _client.PostAsJsonAsync(route, body, cancellationToken);

            if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            {
                return MUplinkResult<TResponse>.Refused("The API rejected this key.");
            }

            if (!response.IsSuccessStatusCode)
            {
                return MUplinkResult<TResponse>.Refused($"The API answered {(int)response.StatusCode}.");
            }

            var payload = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);

            if (payload is null) return MUplinkResult<TResponse>.Malformed("The API returned an empty body.");

            return MUplinkResult<TResponse>.Reached(payload);
        }
        catch (HttpRequestException failure)
        {
            return MUplinkResult<TResponse>.Unreachable(failure.Message);
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            // Report the timeout actually in force on this client, not the default constant.
            return MUplinkResult<TResponse>.Unreachable($"No answer within {_client.Timeout.TotalSeconds:0}s.");
        }
        catch (System.Text.Json.JsonException failure)
        {
            return MUplinkResult<TResponse>.Malformed(failure.Message);
        }
    }
}
