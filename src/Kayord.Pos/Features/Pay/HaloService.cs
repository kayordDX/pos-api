using System.Net;
using System.Text.Json;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Kayord.Pos.Features.Pay.Dto;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay;

public class HaloService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _dbContext;
    private readonly EncryptionService _encryption;

    public HaloService(HttpClient httpClient, AppDbContext dbContext, EncryptionService encryption)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
        _encryption = encryption;
    }

    public async Task<Result<GetLink.Response>> GetLink(decimal amount, int tableBookingId, string userId, int outletId)
    {
        Guid r = Guid.NewGuid();
        await _dbContext.HaloReference.AddAsync(new HaloReference { Id = r, TableBookingId = tableBookingId, UserId = userId });
        await _dbContext.SaveChangesAsync();

        var haloConfig = await Halo.GetHaloConfig(outletId, _dbContext, _encryption);

        HaloLog log = new()
        {
            CreatedBy = userId,
            Type = "GetLink"
        };
        try
        {
            GetLinkRequestDto requestBody = new()
            {
                MerchantId = haloConfig.MerchantId,
                PaymentReference = r.ToString(),
                Amount = amount,
                Timestamp = DateTime.UtcNow.ToString(),
                CurrencyCode = "ZAR",
                IsConsumerApp = false,
                image = new GetLinkImageDto
                {
                    Required = false
                }
            };
            string? request = JsonSerializer.Serialize(requestBody);
            log.Request = request;
            log.RequestUrl = "consumer/qrCode";

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "consumer/qrCode");
            requestMessage.Headers.Add("x-api-key", haloConfig.XApiKey);
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(requestBody));
            var response = await _httpClient.SendAsync(requestMessage);
            // using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("consumer/qrCode", requestBody);
            log.StatusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                log.Response = resultString;
                GetLink.Response? result = resultString.Deserialize<GetLink.Response>();
                if (result != null)
                {
                    var haloReference = await _dbContext.HaloReference.FirstOrDefaultAsync(x => x.Id == r);
                    if (haloReference != null)
                    {
                        haloReference.HaloRef = result.reference;
                    }
                    return Result.Ok(result);
                }
            }
            return Result.Fail<GetLink.Response>(response.StatusCode.ToString() + " " + response.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            log.Error = ex.StackTrace;
            return Result.Fail<GetLink.Response>(ex.Message);
        }
        finally
        {
            await _dbContext.HaloLog.AddAsync(log);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Result<StatusResultDto>> GetStatus(string reference, string userId, int outletId)
    {
        HaloLog log = new()
        {
            CreatedBy = userId,
            Type = "GetStatus"
        };

        try
        {
            var haloConfig = await Halo.GetHaloConfig(outletId, _dbContext, _encryption);
            log.RequestUrl = $"consumer/qrCode/{reference}";
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"consumer/qrCode/{reference}");
            requestMessage.Headers.Add("x-api-key", haloConfig.XApiKey);
            var response = await _httpClient.SendAsync(requestMessage);
            // using HttpResponseMessage response = await _httpClient.GetAsync($"consumer/qrCode/{reference}");
            log.StatusCode = (int)response.StatusCode;
            var logResult = await response.Content.ReadAsStringAsync();
            log.Response = logResult;
            StatusResultDto? result = logResult.Deserialize<StatusResultDto>();

            if (result != null)
            {
                if (result.ResponseCode == 0 && result.TransactionId != string.Empty && result.AuthorisationCode != string.Empty)
                {
                    Payment? p = await _dbContext.Payment.FirstOrDefaultAsync(x => x.PaymentReference == result.PaymentReference);
                    if (p == null)
                    {
                        HaloReference? hRef = await _dbContext.HaloReference.FirstOrDefaultAsync(x => x.Id.ToString() == result.PaymentReference);
                        if (hRef != null)
                        {
                            await new PaymentCompletedEvent
                            {
                                Amount = result.Amount,
                                PaymentReference = result.PaymentReference,
                                UserId = userId,
                                TableBookingId = hRef.TableBookingId
                            }.PublishAsync(Mode.WaitForNone);
                        }
                    }

                }
                return Result.Ok(result);
            }
            return Result.Fail<StatusResultDto>("Empty Response");
        }
        catch (Exception ex)
        {
            log.Error = ex.StackTrace;
            return Result.Fail<StatusResultDto>(ex.Message);
        }
        finally
        {
            await _dbContext.HaloLog.AddAsync(log);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> TestConfig(int configId)
    {
        try
        {
            var haloConfig = await Halo.GetHaloSpecificConfig(configId, _dbContext, _encryption);
            var reference = Guid.NewGuid();
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"consumer/qrCode/{reference}");
            requestMessage.Headers.Add("x-api-key", haloConfig.XApiKey);
            var response = await _httpClient.SendAsync(requestMessage);
            return response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound;
        }
        catch
        {
            return false;
        }
    }
}