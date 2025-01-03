using System.Text.Json;
using GrammarCheckService.Models;

namespace GrammarCheckService.Services;

public class GrammarChecker
{
    private readonly HttpClient _httpClient;

    public GrammarChecker()
    {
        _httpClient = new HttpClient();
    }

    public async Task<GrammarResponse> CheckGrammar(string text, string language)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new GrammarResponse();
        }

        var values = new Dictionary<string, string>
        {
            { "text", text },
            { "language", language ?? "auto" }
        };
        var content = new FormUrlEncodedContent(values);

        var response = await _httpClient.PostAsync("https://api.languagetool.org/v2/check", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"LanguageTool API error: {response.StatusCode}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<LanguageToolResponse>(responseString);

        var grammarResponse = new GrammarResponse();

        foreach (var match in result!.Matches)
        {
            grammarResponse.Errors.Add(new GrammarError
            {
                Sentence = match.Context?.Text,
                Message = match.Message
            });
        }

        return grammarResponse;
    }
}
