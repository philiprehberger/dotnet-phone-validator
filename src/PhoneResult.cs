namespace Philiprehberger.PhoneValidator;

/// <summary>
/// Represents the result of validating a phone number, including parsed components.
/// </summary>
/// <param name="IsValid">Whether the phone number is valid.</param>
/// <param name="Error">Error message if the number is invalid; otherwise <c>null</c>.</param>
/// <param name="E164">The number in E.164 format if valid; otherwise <c>null</c>.</param>
/// <param name="CountryCode">The dialing country code (e.g. "1" for US) if detected; otherwise <c>null</c>.</param>
/// <param name="NationalNumber">The national portion of the number without the country code; otherwise <c>null</c>.</param>
public record PhoneResult(
    bool IsValid,
    string? Error,
    string? E164,
    string? CountryCode,
    string? NationalNumber);
