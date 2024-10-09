namespace Kayord.Pos.Features.Role.AddUserInRole
{
    public class Request
    {
        public string UserId { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}