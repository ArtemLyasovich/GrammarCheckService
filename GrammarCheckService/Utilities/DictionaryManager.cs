using NHunspell;

namespace GrammarCheckService.Utilities;

public class DictionaryManager
{
    private readonly string _localPath = AppContext.BaseDirectory;
    private readonly string _remoteRepo = "https://github.com/wooorm/dictionaries/blob/main/dictionaries";

    private readonly Dictionary<string, Hunspell> _loadedDictionaries = new();

    public async Task<Hunspell> GetDictionary(string language)
    {
        if (_loadedDictionaries.ContainsKey(language))
            return _loadedDictionaries[language];

        var affPath = Path.Combine($"{_localPath}/{language}", $"index.aff");
        var dicPath = Path.Combine($"{_localPath}/{language}", $"index.dic");

        if (!File.Exists(affPath) || !File.Exists(dicPath))
            await DownloadDictionary(language, affPath, dicPath);

        var hunspell = new Hunspell(affPath, dicPath);
        _loadedDictionaries[language] = hunspell;

        return hunspell;
    }

    private async Task DownloadDictionary(string language, string affPath, string dicPath)
    {
        using var client = new HttpClient();

        var affContent = await client.GetStringAsync($"{_remoteRepo}/{language}/index.aff");
        File.WriteAllText(affPath, affContent);

        var dicContent = await client.GetStringAsync($"{_remoteRepo}/{language}/index.dic");
        File.WriteAllText(dicPath, dicContent);
    }
}

