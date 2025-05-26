namespace Kayord.Pos.DTO
{
    public class OptionDTO
    {
        public int OptionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int PositionId { get; set; }
        public int OptionGroupId { get; set; }
        public bool IsAvailable { get; set; }
        public OptionGroupBasicDTO OptionGroup { get; set; } = default!;
    }
}