# SunamoText

Working with text, e.g. converting decoded unicode strings `\u{code}` to one character.

## Overview

SunamoText is part of the Sunamo package ecosystem, providing modular, platform-independent utilities for .NET development.

### Key Classes

- **FormatOfString** - Parsing and validating string formats using pipe-delimited templates.
- **UnicodeHelper** - Decoding Unicode escape sequences in strings.

### Key Methods

- `GetParsedParts(text, format)` - Extracts variable parts from text using a pipe-delimited format template.
- `HasFormat(text, format, isUsingWildcard)` - Checks whether text matches a given pipe-delimited format.
- `DeescapeDecodeUnicode(text)` - Decodes `\uXXXX` escape sequences into their character representations.

## Installation

```bash
dotnet add package SunamoText
```

## Links

- [NuGet](https://www.nuget.org/profiles/sunamo)
- [GitHub](https://github.com/sunamo/PlatformIndependentNuGetPackages)
- [Developer site](https://sunamo.cz)

## Target Frameworks

`net10.0;net9.0;net8.0`

## License

MIT
