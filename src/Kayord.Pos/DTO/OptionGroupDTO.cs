namespace Kayord.Pos.DTO
{
    public class OptionGroupDTO
    {
        public int OptionGroupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MinSelections { get; set; }
        public int MaxSelections { get; set; }
        public ICollection<OptionDTO> Options { get; set; } = default!;
    }
}