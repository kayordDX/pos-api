using System.Text.Json;
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

    public async Task<Status> GetStatus()
    {
        var request = await _httpClient.GetAsync("/session/status/kayord");
        if (request.IsSuccessStatusCode)
        {
            var status = await request.Content.ReadFromJsonAsync<Status>();
            if (status != null)
            {
                return status;
            }
        }
        throw new Exception("Could not get status");
    }

    public async Task<ChatsResponse> GetChats()
    {
        var request = await _httpClient.GetAsync("/client/getChats/kayord");
        if (request.IsSuccessStatusCode)
        {
            var chats = await request.Content.ReadFromJsonAsync<ChatsResponse>();
            if (chats != null)
            {
                return chats;
            }
        }
        throw new Exception("Could not get chats");
    }

    public async Task<NumberIdResponse> GetNumberId(string number) => await GetNumberId(number);

    public async Task<NumberIdResponse> GetNumberId(string number, string? countryCode)
    {
        if (countryCode == null) countryCode = "27";
        string finalNumber = countryCode + number;
        NumberIdRequest request = new() { Number = finalNumber };
        var resp = await _httpClient.PostAsJsonAsync("/client/getNumberId/kayord", request);
        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<NumberIdResponse>();
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("Could not get number");
    }

    public async Task<SendMessageResponse> SendMessage(SendMessageRequest request)
    {
        var resp = await _httpClient.PostAsJsonAsync("/client/sendMessage/kayord", request);
        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<SendMessageResponse>();
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("Could not send message");
    }

    public async Task<SendMessageResponse> SendFile(SendFileRequest request)
    {
        var resp = await _httpClient.PostAsJsonAsync("/client/sendMessage/kayord", request);
        if (resp.IsSuccessStatusCode)
        {
            var result = await resp.Content.ReadFromJsonAsync<SendMessageResponse>();
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("Could not send message");
    }
}