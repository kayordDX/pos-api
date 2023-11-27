using FluentValidation;

namespace Kayord.Pos.Features.Table.Create
{
    public class Request
    {
        public string Name { get; set; } = string.Empty;
        public int SectionId { get; set; }
        public int Capacity { get; set; }
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("TableName is required");
        }
    }
}