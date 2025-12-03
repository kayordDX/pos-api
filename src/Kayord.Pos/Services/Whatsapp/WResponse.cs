namespace Kayord.Pos.Services.Whatsapp;

public class WResponse<T> where T : new()
{
    public int Code { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; } = new T();
}