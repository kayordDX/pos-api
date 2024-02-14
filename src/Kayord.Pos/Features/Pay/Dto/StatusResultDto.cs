namespace Kayord.Pos.Features.Pay.Dto;

public class StatusResultDto
{
    public string QrCodeState { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string PaymentReference { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
}