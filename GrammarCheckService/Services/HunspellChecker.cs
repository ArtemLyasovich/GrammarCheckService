using GrammarCheckService.Utilities;

namespace GrammarCheckService.Services;

public class HunspellChecker
{
    private readonly DictionaryManager _dictionaryManager = new();

    public async Task<SpellingResponse> CheckSpelling(TextRequest request)
    {
        var hunspell = await _dictionaryManager.GetDictionary(request.Language);

        var words = request.Text.Split([' ', '.', ',', '!', '?'], StringSplitOptions.RemoveEmptyEntries);
        var errors = new List<SpellingError>();

        foreach (var word in words)
        {
            if (!hunspell.Spell(word))
            {
                var suggestions = hunspell.Suggest(word);
                errors.Add(new SpellingError
                {
                    Word = word,
                    Suggestions = { suggestions }
                });
            }
        }

        var response = new SpellingResponse
        {
            Errors = { errors }
        };

        return response;
    }
}
