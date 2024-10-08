using System.Text.Json;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Config;
using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Kayord.Pos.Features.Pay.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Kayord.Pos.Features.Pay;

public class HaloService
{
    private readonly HttpClient _httpClient;
    private readonly HaloConfig _haloConfig;
    private readonly AppDbContext _dbContext;

    public HaloService(HttpClient httpClient, IOptions<HaloConfig> haloConfig, AppDbContext dbContext)
    {
        _httpClient = httpClient;
        _haloConfig = haloConfig.Value;
        _dbContext = dbContext;
    }

    public async Task<Result<GetLink.Response>> GetLink(decimal amount, int tableBookingId, string userId)
    {
        Guid r = Guid.NewGuid();
        await _dbContext.HaloReference.AddAsync(new HaloReference { Id = r, TableBookingId = tableBookingId, UserId = userId });
        await _dbContext.SaveChangesAsync();
        HaloLog log = new()
        {
            CreatedBy = userId,
            Type = "GetLink"
        };
        try
        {
            GetLinkRequestDto requestBody = new()
            {
                MerchantId = _haloConfig.MerchantId ?? "",
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

            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("consumer/qrCode", requestBody);
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

    public async Task<Result<StatusResultDto>> GetStatus(string reference, string userId)
    {
        HaloLog log = new()
        {
            CreatedBy = userId,
            Type = "GetStatus"
        };
        try
        {
            log.RequestUrl = $"consumer/qrCode/{reference}";
            using HttpResponseMessage response = await _httpClient.GetAsync($"consumer/qrCode/{reference}");
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
}