using FluentValidation;

namespace Kayord.Pos.Features.Stock.OrderItem.LastPrice;

public class Request
{
    public int StockId { get; set; }
    public int StockOrderId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.StockId).NotEmpty().WithMessage("StockId is required");
        RuleFor(v => v.StockOrderId).NotEmpty().WithMessage("StockOrderId is required");
    }
}