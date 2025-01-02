using GrammarCheckService;
using Microsoft.Extensions.Logging;

namespace GrammarCheckTests;

[TestFixture]
public class GrammarCheckServiceTests
{
    private GrammarCheckService.Services.GrammarCheckService _service;

    [SetUp]
    public void Setup()
    {
        var logger = new LoggerFactory().CreateLogger<GrammarCheckService.Services.GrammarCheckService>();
        _service = new GrammarCheckService.Services.GrammarCheckService(logger);
    }

    [Test]
    public async Task CheckSpelling_ShouldReturnEmptyResponse()
    {
        var request = new TextRequest { Text = "Test text", Language = "en" };

        var response = await _service.CheckSpelling(request, null!);

        Assert.IsNotNull(response);
        Assert.IsEmpty(response.Errors);
    }

    [Test]
    public async Task CheckGrammar_ShouldReturnEmptyResponse()
    {
        var request = new TextRequest { Text = "This is a test sentence.", Language = "en" };

        var response = await _service.CheckGrammar(request, null!);

        Assert.IsNotNull(response);
        Assert.IsEmpty(response.Errors);
    }

    [Test]
    public async Task GetSynonyms_ShouldReturnEmptyResponse()
    {
        var request = new SynonymRequest();

        var response = await _service.GetSynonyms(request, null!);

        Assert.IsNotNull(response);
        Assert.IsEmpty(response.Synonyms);
    }

    [Test]
    public async Task CheckSpelling_ShouldWorkForEnglishAndRussian()
    {
        var englishRequest = new TextRequest { Text = "This is a tst", Language = "en" };
        var russianRequest = new TextRequest { Text = "Это тст", Language = "ru" };

        var englishResponse = await _service.CheckSpelling(englishRequest, null!);
        var russianResponse = await _service.CheckSpelling(russianRequest, null!);

        Assert.IsTrue(englishResponse.Errors.Any(x => x.Word == "tst" && x.Suggestions.Any(x => x == "test")));
        Assert.IsTrue(russianResponse.Errors.Any(x => x.Word == "тст" && x.Suggestions.Any(x => x == "тест")));
    }

    [Test]
    public async Task CheckSpelling_ShouldWorkForFrenchAndGerman()
    {
        var frenchRequest = new TextRequest { Text = "Ceci est un tst", Language = "fr" };
        var germanRequest = new TextRequest { Text = "Dies ist ein tst", Language = "de" };

        var frenchResponse = await _service.CheckSpelling(frenchRequest, null!);
        var germanResponse = await _service.CheckSpelling(germanRequest, null!);

        Assert.IsTrue(Directory.Exists($"{AppContext.BaseDirectory}fr"));
        Assert.IsTrue(Directory.Exists($"{AppContext.BaseDirectory}de"));
    }
}