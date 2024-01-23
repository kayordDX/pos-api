using FluentValidation;
namespace Kayord.Pos.Features.Role.AddUserInRole
{
    public class Request
    {
     public string UserId { get; set; }
     public int RoleId { get; set; }
    }
}