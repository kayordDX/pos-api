namespace Kayord.Pos.Features.User.GetStatus;

public class Response
{
    public int OutletId { get; set; }
    public string? OutletName { get; set; }
    public bool ClockedIn { get; set; }
    public int SalesPeriodId { get; set; }
    public SalesPeriodDTO? SalesPeriod { get; set; }
    public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
    public List<DivisionDTO> Divisions { get; set; } = new List<DivisionDTO>();
    public bool hasNotification { get; set; }
    public int StatusId { get; set; }
}

public class SalesPeriodDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int OutletId { get; set; }
}

public class RoleDTO
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string RoleType { get; set; } = string.Empty;
}

public class DivisionDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}