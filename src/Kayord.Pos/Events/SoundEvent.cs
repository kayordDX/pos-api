namespace Kayord.Pos.Events;

public class SoundEvent
{
    public int OutletId { get; set; }
    public List<int>? Divisions { get; set; }
}