using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Config;
using Kayord.Pos.Features.Pay.Dto;
using Microsoft.Extensions.Options;

namespace Kayord.Pos.Features.Pay;

public class HaloService
{
    private readonly HttpClient _httpClient;
    private readonly HaloConfig _haloConfig;

    public HaloService(HttpClient httpClient, IOptions<HaloConfig> haloConfig)
    {
        _httpClient = httpClient;
        _haloConfig = haloConfig.Value;
    }

    public async Task<Result<GetLink.Response>> GetLink(string name)
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("consumer/qrCode", new Dto.GetLinkRequestDto()
            {
                MerchantId = _haloConfig.MerchantId ?? "",
                PaymentReference = Guid.NewGuid().ToString(),
                Amount = 23,
                Timestamp = DateTime.UtcNow.ToString(),
                CurrencyCode = "ZAR",
                IsConsumerApp = false,
                image = new GetLinkImageDto
                {
                    Required = false
                }
            });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetLink.Response>();
                if (result != null)
                    return Result.Ok(result);
            }
            return Result.Fail<GetLink.Response>(response.StatusCode.ToString() + " " + response.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            return Result.Fail<GetLink.Response>(ex.Message);
        }
    }

    public async Task<Result<StatusResultDto>> GetStatus(string reference)
    {
        try
        {
            StatusResultDto? result = await _httpClient.GetFromJsonAsync<StatusResultDto>($"consumer/qrCode/{reference}");

            if (result != null)
            {
                return Result.Ok(result);
            }
            return Result.Fail<StatusResultDto>("Empty Response");
        }
        catch (Exception ex)
        {
            return Result.Fail<StatusResultDto>(ex.Message);
        }
    }
}