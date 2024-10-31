using FluentValidation;

namespace Kayord.Pos.Features.Pay.PayConfig.Create;

public class Request
{
    public int OutletId { get; set; }
    public string XApiKey { get; set; } = string.Empty;
    public string MerchantId { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.XApiKey).NotEmpty().WithMessage("XApiKey is required");
        RuleFor(v => v.XApiKey).NotEmpty().WithMessage("MerchantId is required");
    }
}