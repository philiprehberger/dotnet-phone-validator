namespace Philiprehberger.PhoneValidator;

/// <summary>
/// Provides static methods for validating, formatting, and analyzing international phone numbers.
/// </summary>
public static class Phone
{
    /// <summary>
    /// Determines whether the specified phone number is valid according to E.164 rules.
    /// </summary>
    /// <param name="number">The phone number to validate. May include a leading '+', spaces, hyphens, or parentheses.</param>
    /// <returns><c>true</c> if the number is a valid E.164 phone number; otherwise <c>false</c>.</returns>
    public static bool IsValid(string number)
    {
        return Validate(number).IsValid;
    }

    /// <summary>
    /// Validates a phone number and returns detailed parsing results including the E.164 representation,
    /// country code, and national number.
    /// </summary>
    /// <param name="number">The phone number to validate. May include a leading '+', spaces, hyphens, or parentheses.</param>
    /// <returns>A <see cref="PhoneResult"/> containing validation status and parsed components.</returns>
    public static PhoneResult Validate(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            return new PhoneResult(false, "Phone number must not be empty.", null, null, null);
        }

        var digits = StripToDigits(number);

        if (digits.Length == 0)
        {
            return new PhoneResult(false, "Phone number contains no digits.", null, null, null);
        }

        // E.164 numbers must have a '+' prefix conceptually and 1-15 digits
        if (digits.Length > 15)
        {
            return new PhoneResult(false, "Phone number exceeds maximum E.164 length of 15 digits.", null, null, null);
        }

        // If number doesn't start with '+', assume it needs a country code context
        // For validation, we require the number to include the country code (start with '+' or digits that match a country code)
        var hasPlus = number.TrimStart().StartsWith('+');

        if (!hasPlus)
        {
            return new PhoneResult(false, "Phone number must include a country code (prefix with '+').", null, null, null);
        }

        if (digits.Length < 3)
        {
            return new PhoneResult(false, "Phone number is too short.", null, null, null);
        }

        // Try to match a country rule
        var match = FindCountryRule(digits);

        if (match is null)
        {
            // Even without a known country, validate basic E.164 constraints
            var e164 = $"+{digits}";
            return new PhoneResult(true, null, e164, null, null);
        }

        var (rule, nationalNumber) = match.Value;

        if (nationalNumber.Length < rule.MinLength)
        {
            return new PhoneResult(false, $"National number is too short for {rule.CountryName} (minimum {rule.MinLength} digits).", null, rule.CountryCode, nationalNumber);
        }

        if (nationalNumber.Length > rule.MaxLength)
        {
            return new PhoneResult(false, $"National number is too long for {rule.CountryName} (maximum {rule.MaxLength} digits).", null, rule.CountryCode, nationalNumber);
        }

        var formattedE164 = $"+{digits}";
        return new PhoneResult(true, null, formattedE164, rule.CountryCode, nationalNumber);
    }

    /// <summary>
    /// Formats a phone number in the specified format. Throws if the number is invalid.
    /// </summary>
    /// <param name="number">The phone number to format. Must include a country code prefix.</param>
    /// <param name="format">The desired output format. Defaults to <see cref="PhoneFormat.E164"/>.</param>
    /// <returns>The formatted phone number string.</returns>
    /// <exception cref="ArgumentException">Thrown when the phone number is invalid.</exception>
    public static string Format(string number, PhoneFormat format = PhoneFormat.E164)
    {
        var result = Validate(number);

        if (!result.IsValid)
        {
            throw new ArgumentException(result.Error, nameof(number));
        }

        var digits = StripToDigits(number);

        return format switch
        {
            PhoneFormat.E164 => result.E164!,
            PhoneFormat.International => FormatInternational(digits, result.CountryCode, result.NationalNumber),
            PhoneFormat.National => FormatNational(result.NationalNumber ?? digits),
            _ => result.E164!
        };
    }

    /// <summary>
    /// Strips non-digit characters (except a leading '+'), trims whitespace, and returns a cleaned
    /// phone string ready for validation.
    /// </summary>
    /// <param name="number">The raw phone number input to normalize.</param>
    /// <returns>A cleaned phone string containing only digits with an optional leading '+'.</returns>
    public static string Normalize(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            return string.Empty;
        }

        var trimmed = number.Trim();
        var hasPlus = trimmed.StartsWith('+');
        var digits = new string(trimmed.Where(char.IsDigit).ToArray());

        return hasPlus ? $"+{digits}" : digits;
    }

    /// <summary>
    /// Detects the country name associated with a phone number based on its country dialing code.
    /// </summary>
    /// <param name="number">The phone number to analyze. Must include a country code prefix.</param>
    /// <returns>The country name if detected; otherwise <c>null</c>.</returns>
    public static string? DetectCountry(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            return null;
        }

        var digits = StripToDigits(number);
        var match = FindCountryRule(digits);

        return match?.Rule.CountryName;
    }

    private static string StripToDigits(string input)
    {
        return new string(input.Where(char.IsDigit).ToArray());
    }

    private static (CountryRule Rule, string NationalNumber)? FindCountryRule(string digits)
    {
        // Try 3-digit codes first, then 2-digit, then 1-digit for most specific match
        for (var length = 3; length >= 1; length--)
        {
            if (digits.Length <= length)
            {
                continue;
            }

            var prefix = digits[..length];

            foreach (var (code, rule) in CountryRules.Rules)
            {
                if (code == prefix)
                {
                    return (rule, digits[length..]);
                }
            }
        }

        return null;
    }

    private static string FormatInternational(string digits, string? countryCode, string? nationalNumber)
    {
        if (countryCode is null || nationalNumber is null)
        {
            return $"+{digits}";
        }

        // Group national number into blocks of 3-4 digits
        var groups = GroupDigits(nationalNumber);
        return $"+{countryCode} {string.Join(" ", groups)}";
    }

    private static string FormatNational(string nationalNumber)
    {
        var groups = GroupDigits(nationalNumber);
        return string.Join(" ", groups);
    }

    private static IEnumerable<string> GroupDigits(string digits)
    {
        var groups = new List<string>();
        var remaining = digits;

        while (remaining.Length > 0)
        {
            var chunkSize = remaining.Length > 4 ? 3 : remaining.Length;
            groups.Add(remaining[..chunkSize]);
            remaining = remaining[chunkSize..];
        }

        return groups;
    }
}
