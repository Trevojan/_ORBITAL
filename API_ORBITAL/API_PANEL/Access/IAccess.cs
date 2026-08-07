namespace API_PANEL.Access;

/// <summary>Anything the panel has identified and scoped.</summary>
public interface IAccessed
{
    string Principal { get; }

    EAccessScope Scope { get; }
}

/// <summary>
/// The authentication contract — the reason a _PANEL exists at all.
/// Kept behind an interface so the demo key store can be swapped for a real one
/// without touching a single endpoint.
/// </summary>
public interface IAccessPolicy
{
    MAccess Resolve(string? presentedKey);

    bool Permits(IAccessed access, EAccessScope required);
}
