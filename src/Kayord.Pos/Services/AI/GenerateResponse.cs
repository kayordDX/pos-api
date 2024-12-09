using System.Text.Json.Serialization;

namespace Kayord.Pos.Services.AI;

public class GenerateResponse
{
    [JsonPropertyName("candidates")]
    public List<Candidate> Candidates { get; set; } = new();
    [JsonPropertyName("usageMetadata")]
    public UsageMetadata? UsageMetadata { get; set; }
    [JsonPropertyName("modelVersion")]
    public string ModelVersion { get; set; } = string.Empty;
}

public class Candidate
{
    [JsonPropertyName("content")]
    public Content Content { get; set; } = new();
}

public class Content
{
    [JsonPropertyName("parts")]
    public List<Part> Parts { get; set; } = new();
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
}

public class Part
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}

public class UsageMetadata
{
    [JsonPropertyName("promptTokenCount")]
    public int PromptTokenCount { get; set; }
    [JsonPropertyName("totalTokenCount")]
    public int TotalTokenCount { get; set; }
}