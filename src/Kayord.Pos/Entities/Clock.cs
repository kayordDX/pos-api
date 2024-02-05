namespace Kayord.Pos.Entities;

public class Clock
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
}