namespace Philiprehberger.PhoneValidator;

/// <summary>
/// Specifies the format for phone number output.
/// </summary>
public enum PhoneFormat
{
    /// <summary>
    /// E.164 format: +{country code}{number} with no spaces or separators.
    /// </summary>
    E164,

    /// <summary>
    /// International format: +{country code} {number} with spaces between groups.
    /// </summary>
    International,

    /// <summary>
    /// National format: number without country code, using local conventions.
    /// </summary>
    National
}
