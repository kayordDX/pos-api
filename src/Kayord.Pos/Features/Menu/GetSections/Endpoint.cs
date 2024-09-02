using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Menu.GetSections
{
    public class GetMenusSectionsEndpoint : Endpoint<Request, Response>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<GetMenusSectionsEndpoint> _logger;
        private readonly RedisClient _redisClient;

        public GetMenusSectionsEndpoint(AppDbContext dbContext, ILogger<GetMenusSectionsEndpoint> logger, RedisClient redisClient)
        {
            _dbContext = dbContext;
            _logger = logger;
            _redisClient = redisClient;
        }

        public override void Configure()
        {
            Get("/menu/sections");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            string cacheKey = $"menu:sections:{req.MenuId}:{req.SectionId}";
            var cachedResponse = await _redisClient.GetObjectAsync<Response>(cacheKey);
            if (cachedResponse != null)
            {
                await SendAsync(cachedResponse);
                return;
            }

            int? parentId = null;
            if (req.SectionId > 0)
            {
                parentId = req.SectionId;
            }

            var sections = await _dbContext.MenuSection
                .Where(x => x.MenuId == req.MenuId && x.ParentId == parentId)
                .OrderBy(x => x.PositionId)
                .ProjectToDto()
                .ToListAsync();

            var parents = await _dbContext.MenuSection
                .Where(x => x.MenuSectionId == req.SectionId)
                .OrderBy(x => x.PositionId)
                .ProjectToDto()
                .ToListAsync();

            Response response = new() { Parents = parents, Sections = sections };
            await _redisClient.SetObjectAsync(cacheKey, response);
            await SendAsync(response);
        }
    }
}
