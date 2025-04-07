namespace Kayord.Pos.Entities;

public class StockAllocate
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
    public int ToOutletId { get; set; }
    public Outlet ToOutlet { get; set; } = default!;
    public string Comment { get; set; } = string.Empty;
    public int StockAllocateStatusId { get; set; }
    public StockAllocateStatus StockAllocateStatus { get; set; } = default!;
    public int FromDivisionId { get; set; }
    public Division FromDivision { get; set; } = default!;
    public int ToDivisionId { get; set; }
    public Division ToDivision { get; set; } = default!;
    public string AssignedUserId { get; set; } = string.Empty;
    public User? AssignedUser { get; set; }
    public string FromUserId { get; set; } = string.Empty;
    public User? FromUser { get; set; }
    public DateTime Created { get; set; }
    public DateTime Completed { get; set; }
    public List<StockAllocateItem>? StockAllocateItems { get; set; }
}