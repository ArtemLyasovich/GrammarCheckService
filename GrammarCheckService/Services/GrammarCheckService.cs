using Grpc.Core;

namespace GrammarCheckService.Services;

public class GrammarCheckService : GrammarCheck.GrammarCheckBase
{
    private readonly ILogger<GrammarCheckService> _logger;

    private readonly GrammarChecker _grammarChecker;
    private readonly SynonymsChecker _synonymsChecker;
    private readonly HunspellChecker _hunspellChecker;
    public GrammarCheckService(ILogger<GrammarCheckService> logger)
    {
        _logger = logger;
        _grammarChecker = new GrammarChecker();
        _synonymsChecker = new SynonymsChecker();
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

    public override async Task<GrammarResponse> CheckGrammar(TextRequest request, ServerCallContext context)
    {
        _logger.LogInformation("CheckGrammar method called.");

        try
        {
            var grammarResponse = await _grammarChecker.CheckGrammar(request.Text, request.Language);

            var rpcResponse = new GrammarResponse();
            foreach (var error in grammarResponse.Errors)
            {
                rpcResponse.Errors.Add(new GrammarError
                {
                    Sentence = error.Sentence,
                    Message = error.Message
                });
            }

            return rpcResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"HTTP error during grammar check: {ex}");
            throw new RpcException(new Status(StatusCode.Unavailable, "Grammar service is unavailable"));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error during grammar check: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "An internal error occurred"));
        }
    }

    public override async Task<SynonymResponse> GetSynonyms(SynonymRequest request, ServerCallContext context)
    {
        _logger.LogInformation("GetSynonyms method called.");

        try
        {
            var synonymResponse = await _synonymsChecker.CheckSynonyms(request);

            var rpcResponse = new SynonymResponse();
            

            return rpcResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"HTTP error during synonym check: {ex}");
            throw new RpcException(new Status(StatusCode.Unavailable, "Synonym service is unavailable"));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error during synonym check: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "An internal error occurred"));
        }
    }
}
