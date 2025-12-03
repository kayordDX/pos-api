namespace Kayord.Pos.Services.Whatsapp;

public class CheckResponse
{
    public int Code { get; set; }
    public CheckData Data { get; set; } = new CheckData();
    public bool Success { get; set; }
}

public class CheckData
{
    public List<CheckUser> Users { get; set; } = [];
}

public class CheckUser
{
    public bool IsInWhatsapp { get; set; }
    public string JID { get; set; } = string.Empty;
    public string Query { get; set; } = string.Empty;
    public string VerifiedName { get; set; } = string.Empty;
}
