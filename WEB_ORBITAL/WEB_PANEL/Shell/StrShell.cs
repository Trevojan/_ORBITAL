namespace WEB_PANEL.Shell;

/// <summary>Constants of the Shell concept.</summary>
public readonly struct StrShell
{
    /// <summary>
    /// The JavaScript function the page exposes to report its own width. Declared in
    /// wwwroot/index.html; this is the only interop name the application uses.
    /// </summary>
    public const string MeasureFunction = "orbitalViewportWidth";

    /// <summary>
    /// A browser app cannot hold a secret: everything shipped to the page is readable by
    /// whoever opened it. So there is no key constant here. Requests either go to an origin
    /// that already knows the caller, or the API route being called is anonymous.
    /// </summary>
    public const string AnonymousNote = "no credential is compiled into a browser bundle";

    /// <summary>The satellite the page asks about on load, so a fresh clone shows something real.</summary>
    public const string ProbeDesignator = "ORB-1";

    public const double ProbeAltitudeKm = 550;

    public const double ProbeInclinationDeg = 53;
}
