using FluentValidation;
namespace Kayord.Pos.Features.User.Validate
{
    public class Request
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
    }
}