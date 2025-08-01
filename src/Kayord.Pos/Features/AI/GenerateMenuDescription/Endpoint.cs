using Microsoft.SemanticKernel.ChatCompletion;

namespace Kayord.Pos.Features.AI.GenerateMenuDescription;

public class Endpoint : Endpoint<Request, string?>
{
    private readonly IChatCompletionService _chatCompletionService;

    public Endpoint(IChatCompletionService chatCompletionService)
    {
        _chatCompletionService = chatCompletionService;
    }

    public override void Configure()
    {
        Post("ai/menu-description");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        string prompt = $"""
            You are a restaurant manager.

            Generate a description for a restaurant menu item. 
            The menu it is on is called {req.Menu}.
            And the section is {req.Section}.
            Menu item is called {req.Name}.

            Do not repeat the menu item name as heading.

            Keep it very short but make it sound delicious.
            The output should be in plain text.
        """;
        var result = await _chatCompletionService.GetChatMessageContentAsync(prompt, cancellationToken: ct);
        await Send.OkAsync(result.Content);
    }
}