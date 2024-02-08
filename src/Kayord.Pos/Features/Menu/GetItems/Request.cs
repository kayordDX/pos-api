namespace Kayord.Pos.Features.Menu.GetItems
{
    public class Request
    {
        public int OutletId { get; set; }
        public int SectionId { get; set; }
        public string? Search { get; set; }
    }
}
