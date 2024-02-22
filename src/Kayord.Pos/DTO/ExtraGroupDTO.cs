namespace Kayord.Pos.DTO
{
    public class ExtraGroupDTO
    {
        public int ExtraGroupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ExtraDTO> Extras { get; set; } = default!;
    }
}