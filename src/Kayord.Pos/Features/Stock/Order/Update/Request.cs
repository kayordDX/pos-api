using FluentValidation;

namespace Kayord.Pos.Features.Stock.Order.Update;

public class Request
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int StockLocationId { get; set; }
    public int SupplierId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}