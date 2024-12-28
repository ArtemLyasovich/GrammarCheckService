using GrammarCheckService;
using Grpc.Core;

namespace GrammarCheckService.Services;

public class GrammarCheckService : GrammarCheck.GrammarCheckBase
{
    private readonly ILogger<GrammarCheckService> _logger;
    public GrammarCheckService(ILogger<GrammarCheckService> logger)
    {
        _logger = logger;
    }

    public override Task<SpellingResponse> CheckSpelling(TextRequest request, ServerCallContext context)
    {
        _logger.LogInformation("CheckSpelling method called.");

        return Task.FromResult(new SpellingResponse());
    }

    public override Task<GrammarResponse> CheckGrammar(TextRequest request, ServerCallContext context)
    {
        _logger.LogInformation("CheckGrammar method called.");

        return Task.FromResult(new GrammarResponse());
    }

    public override Task<SynonymResponse> GetSynonyms(SynonymRequest request, ServerCallContext context)
    {
        _logger.LogInformation("GetSynonyms method called.");

        return Task.FromResult(new SynonymResponse());
    }
}
