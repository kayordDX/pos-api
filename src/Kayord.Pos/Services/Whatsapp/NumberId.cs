namespace Kayord.Pos.Services.Whatsapp;

public class NumberIdResponse
{
    public bool Success { get; set; }
    public NumberIdResult? Result { get; set; }
}

public class NumberIdResult
{
    public string Server { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string _serialized { get; set; } = string.Empty;
}

public class NumberIdRequest
{
    public string Number { get; set; } = string.Empty;
}