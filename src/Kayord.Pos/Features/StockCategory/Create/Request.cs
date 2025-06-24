using FluentValidation;

namespace Kayord.Pos.Features.StockCategory.Create
{
    public class Request
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int OutletId { get; set; }

    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("Category Name is required");
        }
    }
}