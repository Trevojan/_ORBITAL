namespace DESKTOP_PANEL.Shell;

/// <summary>Constants of the Shell concept.</summary>
public readonly struct StrShell
{
    /// <summary>Environment variable holding the API key. Never a compiled-in secret.</summary>
    public const string KeyVariable = "ORBITAL_KEY";

    /// <summary>Matches the placeholder in API_PANEL's development settings, for local runs only.</summary>
    public const string DevelopmentKey = "replace-me-local-only";

    /// <summary>
    /// Resolved against the app's own folder, never against whatever directory it
    /// happened to be started from — a shortcut, or a <c>dotnet run</c> from the
    /// repository root, starts it from somewhere else entirely.
    /// </summary>
    public const string IconFile = "favicon.ico";

    /// <summary>The satellite the shell asks about on start, so the demo shows something real.</summary>
    public const string ProbeDesignator = "ORB-1";

    public const double ProbeAltitudeKm = 550;

    public const double ProbeInclinationDeg = 53;
}
