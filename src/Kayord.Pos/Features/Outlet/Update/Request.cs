using FluentValidation;

namespace Kayord.Pos.Features.Outlet.Update
{
    public class Request
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BusinessId { get; set; }
        // Add other properties related to updating an outlet
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
            // Add other validation rules for updating an outlet
        }
    }
}