using Kayord.Pos.Services.AI;

namespace Kayord.Pos.Features.Generate;

public class Endpoint : Endpoint<Request, GenerateResponse>
{
    private readonly AIService _aiService;

    public Endpoint(AIService aiService)
    {
        _aiService = aiService;
    }

    public override void Configure()
    {
        Post("ai/generate");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var result = await _aiService.Generate(req.Prompt);
        await SendAsync(result);
    }
}