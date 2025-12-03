using Kayord.Pos.Data;

namespace Kayord.Pos.Services.Whatsapp;

public class WhatsappService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _dbContext;

    public WhatsappService(HttpClient httpClient, AppDbContext dbContext)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
    }

    public async Task<WResponse<SessionStatus>> GetStatus()
    {
        var request = await _httpClient.GetAsync("/session/status");
        if (request.IsSuccessStatusCode)
        {
            var status = await request.Content.ReadFromJsonAsync<WResponse<SessionStatus>>();
            if (status != null)
            {
                return status;
            }
        }
        throw new Exception("Could not get status");
    }

    public async Task<CheckResponse> CheckNumbers(List<string> numbers)
    {
        CheckRequest request = new() { Phone = numbers };
        var resp = await _httpClient.PostAsJsonAsync("/user/check", request);
        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<CheckResponse>();
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("Could not check numbers");
    }

    public async Task<CheckResponse> CheckNumber(string number)
    {
        return await CheckNumbers(new List<string> { number });
    }

    public string GetNumberWithCountryCode(string number, string? countryCode)
    {
        countryCode ??= "27";
        return countryCode + number;
    }

    public async Task<WResponse<ChatResponse>> SendText(TextRequest request)
    {
        var resp = await _httpClient.PostAsJsonAsync("/chat/send/text", request);
        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<WResponse<ChatResponse>>();
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("Could not send message");
    }

    public async Task<WResponse<ChatResponse>> SendDocument(DocumentRequest request)
    {
        var resp = await _httpClient.PostAsJsonAsync("/chat/send/document", request);
        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<WResponse<ChatResponse>>();
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("Could not send message");
    }

    public async Task<WResponse<ChatResponse>> SendImage(ImageRequest request)
    {
        var resp = await _httpClient.PostAsJsonAsync("/chat/send/image", request);
        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<WResponse<ChatResponse>>();
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("Could not send message");
    }

    public async Task<WResponse<QrResponse>> QrCode()
    {
        var request = await _httpClient.GetAsync("/session/qr");
        if (request.IsSuccessStatusCode)
        {
            var qr = await request.Content.ReadFromJsonAsync<WResponse<QrResponse>>();
            if (qr != null)
            {
                return qr;
            }
        }
        throw new Exception("Could not get qr code");
    }

    public async Task<WResponse<SessionLogout>> Logout()
    {
        var request = await _httpClient.PostAsJsonAsync("/session/logout", new { });
        if (request.IsSuccessStatusCode)
        {
            var response = await request.Content.ReadFromJsonAsync<WResponse<SessionLogout>>();
            if (response != null)
            {
                return response;
            }
        }
        throw new Exception("Could not terminate session");
    }

    public async Task<WResponse<SessionConnectResponse>> Connect()
    {
        var request = await _httpClient.PostAsJsonAsync("/session/connect", new SessionConnectRequest());
        if (request.IsSuccessStatusCode)
        {
            var response = await request.Content.ReadFromJsonAsync<WResponse<SessionConnectResponse>>();
            if (response != null)
            {
                return response;
            }
        }
        throw new Exception("Could not start session");
    }
}