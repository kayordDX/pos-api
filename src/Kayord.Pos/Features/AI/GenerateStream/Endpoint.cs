using Microsoft.SemanticKernel.ChatCompletion;

namespace Kayord.Pos.Features.AI.GenerateStream;

public class Endpoint : Endpoint<Request, string?>
{
    private readonly IChatCompletionService _chatCompletionService;

    public Endpoint(IChatCompletionService chatCompletionService)
    {
        _chatCompletionService = chatCompletionService;
    }

    public override void Configure()
    {
        Get("ai/stream");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var aiResponse = _chatCompletionService.GetStreamingChatMessageContentsAsync(req.Prompt, cancellationToken: ct);
        await Send.EventStreamAsync("generate", aiResponse, ct);
    }
}