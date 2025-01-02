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

        var dirPath = $"{_localPath}{language}";
        if (!Directory.Exists(dirPath)) 
        { 
            Directory.CreateDirectory(dirPath); 
        }

        var affPath = Path.Combine(dirPath, $"index.aff");
        var dicPath = Path.Combine(dirPath, $"index.dic");

        if (!File.Exists(affPath) || !File.Exists(dicPath))
            await DownloadDictionary(language, affPath, dicPath);

        var hunspell = new Hunspell(affPath, dicPath);
        _loadedDictionaries[language] = hunspell;

        return hunspell;
    }

    private async Task DownloadDictionary(string language, string affPath, string dicPath)
    {
        var client = new HttpClient();

        byte[] fileBytes = await client.GetByteArrayAsync($"{_remoteRepo}/{language}/index.aff"); 
        await File.WriteAllBytesAsync(affPath, fileBytes);

        fileBytes = await client.GetByteArrayAsync($"{_remoteRepo}/{language}/index.dic");
        await File.WriteAllBytesAsync(dicPath, fileBytes);
    }
}

