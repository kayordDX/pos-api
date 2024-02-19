namespace Kayord.Pos.Features.Pay.Dto;

public class StatusResultDto
{
    public string QrCodeState { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string MerchantTransactionReference { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Disposition { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int ResponseCode { get; set; }
    public string AuthorisationCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string PaymentReference { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
}