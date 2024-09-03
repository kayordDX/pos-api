using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.DTO;
using Microsoft.EntityFrameworkCore;
using Kayord.Pos.Services;
using System.Web;
namespace Kayord.Pos.Features.Menu.GetItems
{
    public class GetMenuItemsEndpoint : Endpoint<Request, List<MenuItemDTOBasic>>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<GetMenuItemsEndpoint> _logger;
        private readonly RedisClient _redisClient;

        public GetMenuItemsEndpoint(AppDbContext dbContext, ILogger<GetMenuItemsEndpoint> logger, RedisClient redisClient)
        {
            _dbContext = dbContext;
            _logger = logger;
            _redisClient = redisClient;
        }

        public override void Configure()
        {
            Get("/menu/items");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            string cacheKey = $"menu:items:{req.MenuId}:{req.SectionId}:{HttpUtility.UrlEncode(req.Search)}";
            var cachedResponse = await _redisClient.GetObjectAsync<List<MenuItemDTOBasic>>(cacheKey);
            if (cachedResponse != null)
            {
                await SendAsync(cachedResponse);
                return;
            }

            IQueryable<MenuItem>? items;
            if (req.SectionId == 0)
            {
                items = _dbContext.MenuItem
                    .Include(m => m.MenuSection)
                    .Where(s => s.MenuSection.MenuId.Equals(req.MenuId));
            }
            else
            {
                var sectionParents = await _dbContext.Database.SqlQuery<MenuParents>($"""
                SELECT * FROM "getMenuSectionChildren"({req.MenuId},{req.SectionId})
                """).Select(s => s.Id).ToListAsync();

                items = _dbContext.MenuItem
                    .Where(e => sectionParents.Contains(e.MenuSectionId));
            }

            if (!string.IsNullOrEmpty(req.Search))
            {
                items = items.Where(p => p.SearchVector.Matches(EF.Functions.ToTsQuery(CreateTsQuery(req.Search))));
            }

            var response = await items
                .Include(m => m.Tags)
                .ProjectToBasicDto()
                .ToListAsync();

            _ = _redisClient.SetObjectAsync(cacheKey, response);
            await SendAsync(response);
        }

        public static string CreateTsQuery(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return string.Empty;
            }
            var tokens = SplitSearchString(searchString);
            tokens = tokens.Select(token =>
            {
                var filteredToken = new string(token.Where(c => char.IsLetterOrDigit(c)).ToArray());
                return filteredToken.Length > 0 ? $"{filteredToken}:*" : filteredToken;
            });
            var tsQuery = string.Join(" & ", tokens.Where(x => x.Length > 0));
            return tsQuery;
        }

        private static IEnumerable<string> SplitSearchString(string searchString)
        {
            return searchString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}



