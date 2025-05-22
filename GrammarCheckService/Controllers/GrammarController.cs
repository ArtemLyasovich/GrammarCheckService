using Microsoft.AspNetCore.Mvc;

namespace GrammarCheckService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GrammarController : ControllerBase
{
    private readonly GrammarCheck.GrammarCheckClient _grpcClient;

    public GrammarController(GrammarCheck.GrammarCheckClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    [HttpPost("spellcheck")]
    public async Task<IActionResult> SpellCheck([FromBody] TextRequestModel model)
    {
        var request = new TextRequest
        {
            Text = model.Text,
            Language = model.Language
        };

        var response = await _grpcClient.CheckSpellingAsync(request);
        return Ok(response);
    }

    [HttpPost("grammarcheck")]
    public async Task<IActionResult> GrammarCheck([FromBody] TextRequestModel model)
    {
        var request = new TextRequest
        {
            Text = model.Text,
            Language = model.Language
        };

        var response = await _grpcClient.CheckGrammarAsync(request);
        return Ok(response);
    }
}

public class TextRequestModel
{
    public string Text { get; set; }
    public string Language { get; set; }
}

