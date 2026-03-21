# Philiprehberger.PhoneValidator

[![CI](https://github.com/philiprehberger/dotnet-phone-validator/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-phone-validator/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.PhoneValidator.svg)](https://www.nuget.org/packages/Philiprehberger.PhoneValidator)
[![License](https://img.shields.io/github/license/philiprehberger/dotnet-phone-validator)](LICENSE)

International phone number validation and formatting based on ITU-T E.164 with country detection.

## Installation

```bash
dotnet add package Philiprehberger.PhoneValidator
```

## Usage

```csharp
using Philiprehberger.PhoneValidator;

var isValid = Phone.IsValid("+14155552671");
// true
```

### Validate a Number

```csharp
using Philiprehberger.PhoneValidator;

var result = Phone.Validate("+442071838750");
// result.IsValid       -> true
// result.E164          -> "+442071838750"
// result.CountryCode   -> "44"
// result.NationalNumber -> "2071838750"
// result.Error         -> null
```

### Format as E.164

```csharp
using Philiprehberger.PhoneValidator;

var e164 = Phone.Format("+1 (415) 555-2671");
// "+14155552671"

var international = Phone.Format("+49 30 123456", PhoneFormat.International);
// "+49 301 234 56"

var national = Phone.Format("+33 1 23 45 67 89", PhoneFormat.National);
// "123 456 789"
```

### Detect Country

```csharp
using Philiprehberger.PhoneValidator;

var country = Phone.DetectCountry("+81 90 1234 5678");
// "Japan"

var unknown = Phone.DetectCountry("+999 123456");
// null
```

## API

### `Phone`

| Method | Description |
|--------|-------------|
| `IsValid(string number)` | Returns `true` if the number is a valid E.164 phone number |
| `Validate(string number)` | Validates and parses the number into a `PhoneResult` |
| `Format(string number, PhoneFormat format)` | Formats the number in the specified format (default E.164) |
| `DetectCountry(string number)` | Returns the country name for the number's dialing code, or `null` |

### `PhoneResult`

| Property | Type | Description |
|----------|------|-------------|
| `IsValid` | `bool` | Whether the phone number is valid |
| `Error` | `string?` | Error message if invalid |
| `E164` | `string?` | Number in E.164 format if valid |
| `CountryCode` | `string?` | Dialing country code (e.g. "1", "44") |
| `NationalNumber` | `string?` | National portion without country code |

### `PhoneFormat`

| Value | Description |
|-------|-------------|
| `E164` | `+{code}{number}` with no separators |
| `International` | `+{code} {grouped number}` with spaces |
| `National` | National number only, grouped with spaces |

## Development

```bash
dotnet build src/Philiprehberger.PhoneValidator.csproj --configuration Release
```

## License

MIT
