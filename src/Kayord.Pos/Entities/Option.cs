namespace Kayord.Pos.Entities
{
    public class Option
    {
        public int OptionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int OptionGroupId { get; set; }
        public OptionGroup OptionGroup { get; set; } = default!;
    }
}