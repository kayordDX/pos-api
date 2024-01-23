using FluentValidation;
namespace Kayord.Pos.Features.User.Validate
{
    public class Response
    {
        public string UserId { get; set; } = string.Empty;
        public List<string>? UserRoles { get; set; }

    }
}