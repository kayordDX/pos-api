namespace Kayord.Pos.Common.Printer.DataValidation;

public class DataConstraint
{
    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public List<int> ValidLengths { get; set; } = new List<int>();

    public string ValidChars { get; set; } = string.Empty;
}

