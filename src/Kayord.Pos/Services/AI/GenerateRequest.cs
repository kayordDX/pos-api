using System.Text.Json.Serialization;

namespace Kayord.Pos.Services.AI;

public class GenerateRequest
{
    [JsonPropertyName("contents")]
    public RequestParts Contents { get; set; } = new();
}

public class RequestParts
{
    [JsonPropertyName("parts")]
    public List<Request> Parts { get; set; } = new();
}

public class Request
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}