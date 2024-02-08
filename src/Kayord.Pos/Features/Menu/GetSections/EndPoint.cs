using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Kayord.Pos.Features.Menu.GetSections
{
    public class GetOutletMenusEndpoint : Endpoint<Request, List<MenuSection>>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<GetOutletMenusEndpoint> _logger;

        public GetOutletMenusEndpoint(AppDbContext dbContext, ILogger<GetOutletMenusEndpoint> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public override void Configure()
        {
            Get("/menu/sections");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            int? parentId = null;
            if (req.SectionId > 0)
            {
                parentId = req.SectionId;
            }

            var sections = _dbContext.MenuSection.Where(x => x.ParentId == parentId);
            var response = await sections.ToListAsync();
            await SendAsync(response);
        }
    }
}
