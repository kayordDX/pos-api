namespace Kayord.Pos.Features.Division.GetUsers
{
    public class Request
    {
        public int DivisionId { get; set; }
        public bool excludeSelf { get; set; }
    }
}
