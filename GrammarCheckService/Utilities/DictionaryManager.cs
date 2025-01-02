using Grpc.Core;
using NHunspell;

namespace GrammarCheckService.Utilities;

public class DictionaryManager
{
    private readonly string _localPath = AppContext.BaseDirectory;
    private readonly string _remoteRepo = "https://github.com/wooorm/dictionaries/blob/main/dictionaries";

    private static readonly HttpClient _httpClient = new();
    private readonly Dictionary<string, Hunspell> _loadedDictionaries = new();

    public async Task<Hunspell> GetDictionary(string language)
    {
        if (_loadedDictionaries.ContainsKey(language))
            return _loadedDictionaries[language];

        var dirPath = Path.Combine(_localPath, language);
        EnsureDirectoryExists(dirPath);

        var affPath = Path.Combine(dirPath, "index.aff");
        var dicPath = Path.Combine(dirPath, "index.dic");

        if (!File.Exists(affPath) || !File.Exists(dicPath))
        {
            await DownloadDictionary(language, affPath, dicPath);
        }

        var hunspell = new Hunspell(affPath, dicPath);
        _loadedDictionaries[language] = hunspell;

        return hunspell;
    }

    private async Task DownloadDictionary(string language, string affPath, string dicPath)
    {
        await DownloadFileAsync($"{_remoteRepo}/{language}/index.aff", affPath);
        await DownloadFileAsync($"{_remoteRepo}/{language}/index.dic", dicPath);
    }

    private static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private static async Task DownloadFileAsync(string url, string path)
    {
        var fileBytes = await _httpClient.GetByteArrayAsync(url);
        await File.WriteAllBytesAsync(path, fileBytes);
    }
}
