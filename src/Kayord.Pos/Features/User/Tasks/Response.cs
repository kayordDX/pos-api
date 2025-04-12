namespace Kayord.Pos.Features.User.Tasks;

using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
public class Response
{

    public int Id { get; set; }
    public int OutletId { get; set; }
    public OutletDTOBasic Outlet { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssignedUserId { get; set; } = string.Empty;
    public UserDTO? AssignedUser { get; set; }
}