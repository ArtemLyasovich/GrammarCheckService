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
}