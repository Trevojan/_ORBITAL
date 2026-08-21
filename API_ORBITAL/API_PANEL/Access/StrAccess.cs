namespace API_PANEL.Access;

/// <summary>Constants of the Access concept.</summary>
public readonly struct StrAccess
{
    public const string HeaderName = "X-Orbital-Key";

    /// <summary>Configuration path holding key-to-scope mappings.</summary>
    public const string KeysSection = "Access:Keys";

    /// <summary>Characters of the key digest used as a pseudonymous principal id.</summary>
    public const int PrincipalDigestLength = 8;

    public const string AnonymousPrincipal = "anonymous";

    /// <summary>Prefix every route in this panel sits behind.</summary>
    public const string ApiPrefix = "/api";
}
