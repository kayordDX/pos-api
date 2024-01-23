using FluentValidation;
namespace Kayord.Pos.Features.User.Login
{
    public class Response
    {
     public string UserId { get; set; }
     public List<string> UserRoles { get; set; }

    }
}