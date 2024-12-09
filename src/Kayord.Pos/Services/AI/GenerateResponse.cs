namespace Kayord.Pos.Services.AI;

public class GenerateResponse
{
    public List<Candidate> Candidates { get; set; } = new();
    public UsageMetadata? UsageMetadata { get; set; }
    public string ModelVersion { get; set; } = string.Empty;
}

public class Candidate
{
    public Content Content { get; set; } = new();
}

public class Content
{
    public List<Part> Parts { get; set; } = new();
    public string Role { get; set; } = string.Empty;
}

public class Part
{
    public string Text { get; set; } = string.Empty;
}

public class UsageMetadata
{
    public int PromptTokenCount { get; set; }
    public int TotalTokenCount { get; set; }
}