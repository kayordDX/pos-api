using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.Features.Table.GetMyBooked;

[Mapper]
public static partial class MapperStatic
{
    public static partial IQueryable<Response> ProjectToDto(this IQueryable<Entities.TableBooking> q);
}