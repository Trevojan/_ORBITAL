using UPLINK_DRIVE.Uplink;

namespace DESKTOP_PANEL.Shell;

/// <summary>Where the desktop is pointed and how that is going. Immutable, like every model.</summary>
public sealed class MShell : IShell
{
    private MShell(Uri baseAddress, EShellState state)
    {
        BaseAddress = baseAddress;
        State = state;
    }

    public Uri BaseAddress { get; }

    public EShellState State { get; }

    public static MShell Targeting(Uri baseAddress)
    {
        ArgumentNullException.ThrowIfNull(baseAddress);

        return new MShell(baseAddress, EShellState.Starting);
    }

    public static MShell Default() => Targeting(StrUplink.DefaultBaseAddress);

    public MShell At(EShellState state) => new(BaseAddress, state);
}
