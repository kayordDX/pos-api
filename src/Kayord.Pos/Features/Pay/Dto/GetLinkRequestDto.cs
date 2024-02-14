namespace Kayord.Pos.Features.Pay.Dto;

public class GetLinkRequestDto
{
    public string MerchantId { get; set; } = string.Empty;
    public string PaymentReference { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Timestamp { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public bool IsConsumerApp { get; set; }
    public GetLinkImageDto image { get; set; } = new GetLinkImageDto();
}