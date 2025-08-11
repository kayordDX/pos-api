namespace Kayord.Pos.Features.User.Tasks;

using Kayord.Pos.DTO;

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
    public DateTime LastModified { get; set; }
    public int ToDivisionId { get; set; }
}