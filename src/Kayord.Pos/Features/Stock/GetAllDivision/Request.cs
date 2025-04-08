using Kayord.Pos.Common.Models;

namespace Kayord.Pos.Features.Stock.GetAllDivision;

public class Request : QueryModel
{
    public int DivisionId { get; set; }
}