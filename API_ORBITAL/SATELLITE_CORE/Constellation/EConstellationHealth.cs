namespace SATELLITE_CORE.Constellation;

/// <summary>Coverage verdict for a whole constellation, derived by <see cref="SConstellation"/>.</summary>
public enum EConstellationHealth
{
    Unknown = 0,
    Healthy,
    Strained,
    Failing,
    Grounded
}
