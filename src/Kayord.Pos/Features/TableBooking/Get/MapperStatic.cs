using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.Features.TableBooking.Get;

[Mapper]
public static partial class MapperStatic
{
    public static partial IQueryable<Response> ProjectToDto(this IQueryable<Entities.TableBooking> q);
}