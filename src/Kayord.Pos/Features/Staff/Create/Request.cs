using FluentValidation;
using Kayord.Pos.Common.Enums;

namespace Kayord.Pos.Features.Staff.Create;

public class Request
{
    public string Name { get; set; } = string.Empty;
    public int BusinessId { get; set; }
    public StaffType StaffType { get; set; }
   
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
    }
}