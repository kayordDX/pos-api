using Kayord.Pos.Common.Models;

namespace Kayord.Pos.Features.Stock.Allocate.GetAll;

public class Request : QueryModel
{
    public int OutletId { get; set; }
}