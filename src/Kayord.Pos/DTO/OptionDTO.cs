namespace Kayord.Pos.Entities
{
    public class OptionDTO
    {
        public int OptionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int OptionGroupId { get; set; }
    }
}