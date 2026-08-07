namespace API_PANEL.Access;

/// <summary>
/// Ordered so that a comparison answers "is this enough?" — see <see cref="SAccess.Permits"/>.
/// </summary>
public enum EAccessScope
{
    None = 0,
    Read = 1,
    Write = 2,
    Operator = 3
}
