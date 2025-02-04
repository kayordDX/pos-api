using Kayord.Pos.Entities;
using Kayord.Pos.Features.Manager.OrderView;

namespace Kayord.Pos.DTO;

public class SupplierDTO
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DivisionId { get; set; }
    public DivisionDTO Division { get; set; } = default!;
}