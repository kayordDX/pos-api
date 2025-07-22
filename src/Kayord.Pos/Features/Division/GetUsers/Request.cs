namespace Kayord.Pos.Features.Division.GetUsers
{
    public class Request
    {
        public int DivisionId { get; set; }
        public bool? ExcludeSelf { get; set; }
    }
}
