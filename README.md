# gRPC Spelling and Grammar Check Service

## Description

This project provides a gRPC service that offers an API for checking the spelling and grammar of text in multiple languages, including Russian and English. The service accepts text requests from clients, analyzes them, and returns a list of detected errors with suggestions for correction. Additionally, the service can suggest synonyms to improve text style.

## Service Functionality

1. **Spelling Check**:
   - Accepts text as input.
   - Returns a list of misspelled words and suggestions for correction.

2. **Grammar Check**:
   - Analyzes text for grammatical errors.
   - Returns a list of sentences with potential issues and recommendations for improvement.

3. **Synonym Suggestions**:
   - Identifies key words in the text.
   - Suggests synonyms to replace key words to enhance text style.

4. **Supported Languages**:
   - Russian.
   - English.
   - Expandable to other languages as needed.

## Technology Stack

- **Programming Language**: C#
- **Framework**: .NET Core/6+
- **Protocol**: gRPC
- **Libraries for Spelling and Grammar Check**:
  - **Hunspell** (for spelling check).
  - **LanguageTool** (for grammar check and synonym suggestions).

## Getting Started

### Prerequisites

- .NET Core SDK 6.0 or higher
- LanguageTool API Key (if required a function to find synonyms)
- gRPC tools for .NET

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/ArtemLyasovich/GrammarCheckService.git
   cd GrammarCheckService
   ```
2. Install the required dependencies:
  ```bash
  dotnet restore
  ```
### Usage

1. Build the project:
```bash
dotnet build ./GrammarCheckService/GrammarCheckService.csproj
```
2. Run the gRPC service:
```bash
dotnet run ./GrammarCheckService/GrammarCheckService.csproj
```
3. Send gRPC requests to the service for spelling and grammar checks, and synonym suggestions.

### Contributing

Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.
