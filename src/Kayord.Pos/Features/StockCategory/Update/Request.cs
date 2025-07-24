using FluentValidation;

namespace Kayord.Pos.Features.StockCategory.Update;

public class Request
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public bool? IsDeleted { get; set; }

}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage("Category Name is required");
    }
}