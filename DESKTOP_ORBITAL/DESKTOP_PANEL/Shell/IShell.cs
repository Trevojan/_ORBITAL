using UPLINK_DRIVE.Uplink;

namespace DESKTOP_PANEL.Shell;

/// <summary>
/// The desktop's composition state. Reuses <see cref="IUplinkTarget"/> from the _DRIVE
/// rather than restating an address property — split, never duplicate, even across rings.
/// </summary>
public interface IShell : IUplinkTarget
{
    EShellState State { get; }
}

/// <summary>Translates a transport outcome into something the shell can show.</summary>
public interface IShellOperations
{
    EShellState StateFor(EUplinkOutcome outcome);
}
