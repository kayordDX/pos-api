using FluentValidation;

namespace Kayord.Pos.Features.Stock.Link.Update;

public class Request
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public decimal Quantity { get; set; }
    public int LinkType { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(v => v.Id).NotEmpty().WithMessage("Stock Id is required");
    }
}