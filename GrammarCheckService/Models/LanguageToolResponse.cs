using System.Text.Json.Serialization;

namespace GrammarCheckService.Models;

public class LanguageToolResponse
{
    [JsonPropertyName("software")]
    public Software Software { get; set; } = new();
    [JsonPropertyName("warnings")]
    public Warnings Warnings { get; set; } = new();
    [JsonPropertyName("language")]
    public Language Language { get; set; } = new();
    [JsonPropertyName("matches")]
    public List<Match> Matches { get; set; } = new();
    [JsonPropertyName("sentenceRanges")]
    public List<int[]> SentenceRanges { get; set; } = new();
    [JsonPropertyName("extendedSentenceRanges")]
    public List<ExtendedSentenceRange> ExtendedSentenceRanges { get; set; } = new();
}

public class Software
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;
    [JsonPropertyName("buildDate")]
    public string BuildDate { get; set; } = string.Empty;
    [JsonPropertyName("apiVersion")]
    public int ApiVersion { get; set; }
    [JsonPropertyName("premium")]
    public bool Premium { get; set; }
    [JsonPropertyName("premiumHint")]
    public string PremiumHint { get; set; } = string.Empty;
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}

public class Warnings
{
    [JsonPropertyName("incompleteResults")]
    public bool IncompleteResults { get; set; }
}

public class Language
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
    [JsonPropertyName("detectedLanguage")]
    public DetectedLanguage DetectedLanguage { get; set; } = new();
}

public class DetectedLanguage
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;
}

public class Match
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("shortMessage")]
    public string ShortMessage { get; set; } = string.Empty;
    [JsonPropertyName("replacement")]
    public List<Replacement> Replacements { get; set; } = new();
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    [JsonPropertyName("length")]
    public int Length { get; set; }
    [JsonPropertyName("context")]
    public Context Context { get; set; } = new();
    [JsonPropertyName("sentence")]
    public string Sentence { get; set; } = string.Empty;
    [JsonPropertyName("type")]
    public Type Type { get; set; } = new();
    [JsonPropertyName("rule")]
    public Rule Rule { get; set; } = new();
    [JsonPropertyName("ignoreForIncompleteSentence")]
    public bool IgnoreForIncompleteSentence { get; set; }
    [JsonPropertyName("contextForSureMatch")]
    public int ContextForSureMatch { get; set; }
}

public class Replacement
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class Context
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    [JsonPropertyName("length")]
    public int Length { get; set; }
}

public class Type
{
    [JsonPropertyName("typeName")]
    public string TypeName { get; set; } = string.Empty;
}

public class Rule
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("subId")]
    public string SubId { get; set; } = string.Empty;
    [JsonPropertyName("sourceFile")]
    public string SourceFile { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("issueType")]
    public string IssueType { get; set; } = string.Empty;
    [JsonPropertyName("urls")]
    public List<Url> Urls { get; set; } = new();
    [JsonPropertyName("category")]
    public Category Category { get; set; } = new();
    [JsonPropertyName("isPremium")]
    public bool IsPremium { get; set; }
}

public class Url
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class Category
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class ExtendedSentenceRange
{
    [JsonPropertyName("from")]
    public int From { get; set; }
    [JsonPropertyName("to")]
    public int To { get; set; }
    [JsonPropertyName("detectedLanguages")]
    public List<DetectedLanguage> DetectedLanguages { get; set; } = new();
}
