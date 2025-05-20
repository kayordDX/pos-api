namespace Kayord.Pos.Features.User.GetStatus;

using Kayord.Pos.Entities;
public class Response
{
    public int OutletId { get; set; }
    public string? OutletName { get; set; }
    public bool ClockedIn { get; set; }
    public int SalesPeriodId { get; set; }
    public SalesPeriod? SalesPeriod { get; set; }
    public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
    public bool hasNotification { get; set; }
}

public class RoleDTO
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string RoleType { get; set; } = string.Empty;
}