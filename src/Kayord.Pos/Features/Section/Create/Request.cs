using FluentValidation;

namespace Kayord.Pos.Features.Section.Create
{
    public class Request
    {
        public string Name { get; set; } = string.Empty;
        // Add other properties related to creating a section
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
            // Add other validation rules for creating a section
        }
    }
}