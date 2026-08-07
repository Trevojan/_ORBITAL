namespace DESKTOP_PANEL.Shell;

/// <summary>Whether the desktop is talking to its API, derived by <see cref="SShell"/>.</summary>
public enum EShellState
{
    Unknown = 0,
    Starting,
    Online,
    Refused,
    Offline
}
