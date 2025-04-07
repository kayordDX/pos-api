using Kayord.Pos.Features.Manager.OrderView;

namespace Kayord.Pos.DTO;

public class StockAllocateDTO
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public OutletDTOBasic Outlet { get; set; } = default!;
    public int ToOutletId { get; set; }
    public OutletDTOBasic ToOutlet { get; set; } = default!;
    public string Comment { get; set; } = string.Empty;
    public int StockAllocateStatusId { get; set; }
    public StockAllocateStatusDTO StockAllocateStatus { get; set; } = default!;
    public int FromDivisionId { get; set; }
    public DivisionDTO FromDivision { get; set; } = default!;
    public int ToDivisionId { get; set; }
    public DivisionDTO ToDivision { get; set; } = default!;
    public string AssignedUserId { get; set; } = string.Empty;
    public UserDTO? AssignedUser { get; set; }
    public string FromUserId { get; set; } = string.Empty;
    public UserDTO? FromUser { get; set; }
    public DateTime Created { get; set; }
    public DateTime Completed { get; set; }
    // public List<StockAllocateItemDTO>? StockAllocateItems { get; set; }
}