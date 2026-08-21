using System.Security.Cryptography;
using System.Text;

namespace API_PANEL.Access;

/// <summary>
/// Resolves a presented key to a scope using configuration, so no credential is compiled in.
/// The principal is a digest, never the key — an access log cannot leak the secret.
/// </summary>
public sealed class SAccess(IConfiguration configuration) : IAccessPolicy
{
    private readonly IConfiguration _configuration =
        configuration ?? throw new ArgumentNullException(nameof(configuration));

    public MAccess Resolve(string? presentedKey)
    {
        if (string.IsNullOrWhiteSpace(presentedKey)) return MAccess.Anonymous;

        var configured = _configuration[$"{StrAccess.KeysSection}:{presentedKey}"];

        if (!Enum.TryParse<EAccessScope>(configured, ignoreCase: true, out var scope)) return MAccess.Anonymous;

        return MAccess.Granted(PseudonymOf(presentedKey), scope);
    }

    public bool Permits(IAccessed access, EAccessScope required)
    {
        ArgumentNullException.ThrowIfNull(access);

        return access.Scope >= required;
    }

    private static string PseudonymOf(string key) =>
        Convert.ToHexStringLower(SHA256.HashData(Encoding.UTF8.GetBytes(key)))[..StrAccess.PrincipalDigestLength];
}

/// <summary>
/// The gate every route sits behind. Reads intent from the HTTP verb so no endpoint
/// has to remember to declare its own scope.
/// </summary>
public sealed class SAccessGate(IAccessPolicy policy) : IEndpointFilter
{
    private readonly IAccessPolicy _policy = policy ?? throw new ArgumentNullException(nameof(policy));

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        var request = context.HttpContext.Request;
        var access = _policy.Resolve(request.Headers[StrAccess.HeaderName].FirstOrDefault());
        var required = RequiredFor(request.Method);

        if (!_policy.Permits(access, required))
        {
            return Results.Problem(
                title: "Access denied",
                detail: $"Scope {required} is required for this route.",
                statusCode: StatusCodes.Status401Unauthorized);
        }

        context.HttpContext.Items[nameof(MAccess)] = access;

        return await next(context);
    }

    private static EAccessScope RequiredFor(string method) =>
        HttpMethods.IsGet(method) ? EAccessScope.Read : EAccessScope.Write;
}
