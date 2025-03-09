namespace Kayord.Pos.Features.User.GetStatus;
using Kayord.Pos.Entities;
public class Response
{
    public int OutletId { get; set; }
    public bool ClockedIn { get; set; }
    public int SalesPeriodId { get; set; }
    public SalesPeriod? SalesPeriod { get; set; }
    public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
}

public class RoleDTO
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string AppRoleName { get; set; } = string.Empty;
}