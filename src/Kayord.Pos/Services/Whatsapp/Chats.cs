namespace Kayord.Pos.Services.Whatsapp;

public class ChatsResponse
{
    public bool Success { get; set; }
    public List<Chat> Chats { get; set; } = new List<Chat>();
}

public class Chat
{
    public ChatId Id { get; set; } = new ChatId();
    public string Name { get; set; } = string.Empty;
    public bool IsGroup { get; set; }
    public bool IsReadOnly { get; set; }
    public int UnreadCount { get; set; }
    public int Timestamp { get; set; }
    public bool Pinned { get; set; }
    public int MuteExpiration { get; set; }
    public Message LastMessage { get; set; } = new Message();
}

public class ChatId
{
    public string Server { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string _serialized { get; set; } = string.Empty;
}

public class Message
{
    public MessageId Id { get; set; } = new MessageId();
    public int Ack { get; set; }
    public bool HasMedia { get; set; }
    public string Body { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Timestamp { get; set; }
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string DeviceType { get; set; } = string.Empty;
    public bool IsForwarded { get; set; }
    public int ForwardingScore { get; set; }
    public bool IsStatus { get; set; }
    public bool IsStarred { get; set; }
    public bool FromMe { get; set; }
    public bool HasQuotedMsg { get; set; }
    public bool HasReaction { get; set; }
    public bool IsGif { get; set; }
    // "vCards": [],
    // "mentionedIds": [],
    // "groupMentions": [],
    // "links": []
}

public class MessageId
{
    public bool FromMe { get; set; }
    public string Remote { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string _serialized { get; set; } = string.Empty;
}