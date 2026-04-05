# Changelog

## 0.2.0 (2026-04-05)

- Add `Phone.Normalize()` method that strips non-digit characters (except leading `+`), trims whitespace, and returns a cleaned phone string ready for validation

## 0.1.1 (2026-03-31)

- Standardize README to 3-badge format with emoji Support section
- Update CI actions to v5 for Node.js 24 compatibility
- Add GitHub issue templates, dependabot config, and PR template

## 0.1.0 (2026-03-21)

- Initial release
- Phone number validation against E.164 format
- Formatting in E.164, International, and National formats
- Country detection from dialing code for 30+ countries
- Detailed validation results with error messages
