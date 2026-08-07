namespace API_PANEL.Access;

/// <summary>A resolved caller. Immutable, and never carries the presented key itself.</summary>
public sealed class MAccess : IAccessed
{
    private MAccess(string principal, EAccessScope scope)
    {
        Principal = principal;
        Scope = scope;
    }

    public string Principal { get; }

    public EAccessScope Scope { get; }

    /// <summary>Nobody. The default answer whenever a key is absent, unknown, or malformed.</summary>
    public static MAccess Anonymous { get; } = new(StrAccess.AnonymousPrincipal, EAccessScope.None);

    public static MAccess Granted(string principal, EAccessScope scope)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(principal);

        return new MAccess(principal, scope);
    }
}
