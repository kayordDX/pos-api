namespace Kayord.Pos.Services.Whatsapp;

public class SendMessageRequest
{
    public string ChatId { get; set; } = string.Empty;
    public string ContentType { get; set; } = "string";
    public string Content { get; set; } = string.Empty;
}

public class SendFileRequest
{
    public string ChatId { get; set; } = string.Empty;
    public string ContentType { get; set; } = "MessageMedia";
    public Content Content { get; set; } = new Content();
}

public class Content
{
    public string MimeType { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Filename { get; set; } = string.Empty;
}

public class SendMessageResponse
{
    public bool Success { get; set; }
    public Message Message { get; set; } = new Message();
}