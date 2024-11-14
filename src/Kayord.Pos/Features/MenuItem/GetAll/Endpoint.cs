

using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.MenuItem.GetAll
{
    public class Endpoint : Endpoint<Request, PaginatedList<MenuItemAdminDTO>>
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
            Get("/menuItem");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            int outletId = await Helper.GetUserOutlet(_dbContext, _cu.UserId ?? "");

            // TODO: Filter on outlet
            var menuItems = await _dbContext.MenuItem.ProjectToAdminDto().GetPagedAsync(req, ct);
            await SendAsync(menuItems);
        }
    }
}