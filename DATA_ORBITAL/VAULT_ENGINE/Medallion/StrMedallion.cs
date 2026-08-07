namespace VAULT_ENGINE.Medallion;

/// <summary>Constants of the Medallion concept.</summary>
public readonly struct StrMedallion
{
    public const string DesignatorField = "designator";

    public const string AltitudeField = "altitudeKm";

    public const string CapturedField = "capturedAt";

    /// <summary>Bronze may be anything; silver may not exist without these.</summary>
    public static readonly string[] RequiredFields = [DesignatorField, AltitudeField];

    /// <summary>Fields that must parse as numbers before a record reaches gold.</summary>
    public static readonly string[] MeasurableFields = [AltitudeField];
}

/// <summary>A lesser data group: what one refinement pass achieved.</summary>
public readonly record struct StrRefinementTally(int Bronze, int Silver, int Gold, int Quarantined)
{
    public int Total => Bronze + Silver + Gold + Quarantined;
}
