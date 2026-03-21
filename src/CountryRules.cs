namespace Philiprehberger.PhoneValidator;

/// <summary>
/// Defines validation rules for a single country's phone numbers.
/// </summary>
internal record CountryRule(
    string CountryCode,
    int MinLength,
    int MaxLength,
    string CountryName);

/// <summary>
/// Contains phone number validation rules for major countries worldwide.
/// </summary>
internal static class CountryRules
{
    /// <summary>
    /// Lookup of country dialing codes to their validation rules.
    /// Ordered by specificity (longer codes first) to ensure correct matching.
    /// </summary>
    internal static readonly IReadOnlyList<(string Code, CountryRule Rule)> Rules = new List<(string, CountryRule)>
    {
        // 3-digit codes first
        ("852", new CountryRule("852", 8, 8, "Hong Kong")),
        ("886", new CountryRule("886", 8, 9, "Taiwan")),
        ("353", new CountryRule("353", 7, 9, "Ireland")),
        ("351", new CountryRule("351", 9, 9, "Portugal")),
        ("358", new CountryRule("358", 5, 12, "Finland")),
        ("354", new CountryRule("354", 7, 7, "Iceland")),
        ("372", new CountryRule("372", 7, 8, "Estonia")),
        ("380", new CountryRule("380", 9, 9, "Ukraine")),

        // 2-digit codes
        ("1",  new CountryRule("1",  10, 10, "United States")),
        ("7",  new CountryRule("7",  10, 10, "Russia")),
        ("20", new CountryRule("20", 10, 10, "Egypt")),
        ("27", new CountryRule("27", 9,  9,  "South Africa")),
        ("30", new CountryRule("30", 10, 10, "Greece")),
        ("31", new CountryRule("31", 9,  9,  "Netherlands")),
        ("32", new CountryRule("32", 8,  9,  "Belgium")),
        ("33", new CountryRule("33", 9,  9,  "France")),
        ("34", new CountryRule("34", 9,  9,  "Spain")),
        ("36", new CountryRule("36", 8,  9,  "Hungary")),
        ("39", new CountryRule("39", 6,  11, "Italy")),
        ("41", new CountryRule("41", 9,  9,  "Switzerland")),
        ("43", new CountryRule("43", 4,  12, "Austria")),
        ("44", new CountryRule("44", 10, 10, "United Kingdom")),
        ("45", new CountryRule("45", 8,  8,  "Denmark")),
        ("46", new CountryRule("46", 7,  13, "Sweden")),
        ("47", new CountryRule("47", 8,  8,  "Norway")),
        ("48", new CountryRule("48", 9,  9,  "Poland")),
        ("49", new CountryRule("49", 2,  13, "Germany")),
        ("52", new CountryRule("52", 10, 10, "Mexico")),
        ("55", new CountryRule("55", 10, 11, "Brazil")),
        ("61", new CountryRule("61", 9,  9,  "Australia")),
        ("62", new CountryRule("62", 5,  12, "Indonesia")),
        ("81", new CountryRule("81", 9,  10, "Japan")),
        ("82", new CountryRule("82", 8,  11, "South Korea")),
        ("86", new CountryRule("86", 11, 11, "China")),
        ("90", new CountryRule("90", 10, 10, "Turkey")),
        ("91", new CountryRule("91", 10, 10, "India")),
        ("60", new CountryRule("60", 7,  10, "Malaysia")),
        ("63", new CountryRule("63", 10, 10, "Philippines")),
        ("64", new CountryRule("64", 8,  10, "New Zealand")),
        ("65", new CountryRule("65", 8,  8,  "Singapore")),
        ("66", new CountryRule("66", 9,  9,  "Thailand")),
    }.AsReadOnly();
}
