using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Outlet.GetAllAssigned;

public class Endpoint : EndpointWithoutRequest<List<OutletDTOBasic>>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("/outlet/assigned");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.Database.SqlQuery<OutletDTOBasic>($"""
            select distinct
                o.id, 
                o.name, 
                o.vat_number, 
                o.address, 
                o.company, 
                o.registration
                from outlet o
            join user_role_outlet ur
                on ur.outlet_id = o.id
            where ur.user_id = {_cu.UserId}
        """).ToListAsync(ct);

        await SendAsync(results);
    }
}
