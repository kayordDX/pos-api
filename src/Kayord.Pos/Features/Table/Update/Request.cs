using FluentValidation;

namespace Kayord.Pos.Features.Table.Update
{
    public class Request
    {
        public int TableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SectionId { get; set; }
        public int Capacity { get; set; }
        public int Position { get; set; }

    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.TableId).GreaterThan(0).WithMessage("Table Id must be greater than 0");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Table Name is required");
        }
    }
}