using System.Runtime.CompilerServices;
using System.Text.Json;
using Kayord.Pos.Config;
using Microsoft.Extensions.Options;

namespace Kayord.Pos.Services.AI;

public class AIService
{
    private readonly AppConfig _appConfig;
    private readonly HttpClient _httpClient;

    public AIService(HttpClient httpClient, IOptions<AppConfig> appConfig)
    {
        _httpClient = httpClient;
        _appConfig = appConfig.Value;
    }

    public async Task<GenerateResponse> Generate(string prompt)
    {
        GenerateRequest generateRequest = new()
        {
            Contents = new()
            {
                Parts = new()
                {
                    new ()
                    {
                        Text = prompt
                    }
                }
            }
        };
        var request = await _httpClient.PostAsJsonAsync($"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={_appConfig.GeminiKey}", generateRequest);
        if (request.IsSuccessStatusCode)
        {
            var response = await request.Content.ReadFromJsonAsync<GenerateResponse>();
            if (response != null)
            {
                return response;
            }
        }
        throw new Exception("Could not generate text");
    }

    public async IAsyncEnumerable<string> GenerateStream(string prompt, [EnumeratorCancellation] CancellationToken ct)
    {
        GenerateRequest generateRequest = new()
        {
            Contents = new()
            {
                Parts = new()
                {
                    new ()
                    {
                        Text = prompt
                    }
                }
            }
        };
        var ms = new MemoryStream();
        await JsonSerializer.SerializeAsync(ms, generateRequest);
        ms.Position = 0;

        var request = new HttpRequestMessage(HttpMethod.Post, $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:streamGenerateContent?alt=sse&key={_appConfig.GeminiKey}");
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using var requestContent = new StreamContent(ms);
        request.Content = requestContent;
        requestContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        var the = await response.Content.ReadAsStreamAsync();
        // var content = await response.Content.ReadAsStringAsync();
        var theStreamReader = new StreamReader(the);

        string? theLine = null;
        while (!ct.IsCancellationRequested && (theLine = await theStreamReader.ReadLineAsync()) != null)
        {
            if (string.IsNullOrEmpty(theLine) == false)
            {
                string rr = theLine.Replace("data: ", string.Empty);
                var r = JsonSerializer.Deserialize<GenerateResponse>(rr);
                yield return r?.Candidates.First().Content.Parts.First().Text ?? string.Empty;
            }
        }
    }
}