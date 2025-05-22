using System.Text.Json;
using GrammarCheckService.Models;

namespace GrammarCheckService.Services;

public class SynonymsChecker
{
    private readonly HttpClient _httpClient;

    public SynonymsChecker()
    {
        _httpClient = new HttpClient();
    }

    public async Task<SynonymList> CheckSynonyms(SynonymRequest request) 
    { 
        if (request.Words.Count == 0) 
        {
            return new();
        } 
        
        var wordsQuery = string.Join(",", request.Words); 
        var url = $"https://api.languagetool.org/v2/check?words={wordsQuery}"; 
        var response = await _httpClient.GetStringAsync(url); 
        var synonymResponse = JsonSerializer.Deserialize<LanguageToolResponse>(response);
        return new(); 
    }
}
