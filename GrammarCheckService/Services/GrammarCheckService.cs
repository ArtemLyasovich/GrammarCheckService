using Grpc.Core;

namespace GrammarCheckService.Services;

public class GrammarCheckService : GrammarCheck.GrammarCheckBase
{
    private readonly ILogger<GrammarCheckService> _logger;

    private readonly HunspellChecker _hunspellChecker;
    public GrammarCheckService(ILogger<GrammarCheckService> logger)
    {
        _logger = logger;
        _hunspellChecker = new HunspellChecker();
    }

    public override async Task<SpellingResponse> CheckSpelling(TextRequest request, ServerCallContext context)
    {
        _logger.LogInformation("CheckSpelling method called.");

        SpellingResponse result = new();
        try 
        {
            result = await _hunspellChecker.CheckSpelling(request);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("HttpRequestException: {0}", ex);
            throw new RpcException(new Status(StatusCode.Unavailable, "Network error: " + ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception: {0}", ex);
            throw new RpcException(new Status(StatusCode.Unknown, "Error downloading the file: " + ex.Message));
        }

        return result;
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
