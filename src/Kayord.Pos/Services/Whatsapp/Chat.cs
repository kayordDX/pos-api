namespace Kayord.Pos.Services.Whatsapp;

public class TextRequest
{
    public string Phone { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool LinkPreview { get; set; }
}

public class DocumentRequest
{
    public string Phone { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}

public class ImageRequest
{
    public string Phone { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
}

public class ChatResponse
{
    public string Details { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public int Timestamp { get; set; }
}