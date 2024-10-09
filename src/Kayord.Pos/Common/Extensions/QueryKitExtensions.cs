using Kayord.Pos.Common.Models;
using Microsoft.EntityFrameworkCore;
using QueryKit;
using QueryKit.Configuration;
namespace Kayord.Pos.Common.Extensions;

public static class QueryKitExtensions
{
    public static async Task<PaginatedList<T>> GetPagedAsync<T>(this IQueryable<T> query, QueryModel queryModel, CancellationToken ct) where T : class
    {
        var (itemsQuery, pageNumber, pageSize, count) = await GetPagedResultAsync(query, queryModel, ct);
        return new PaginatedList<T>(await itemsQuery.ToListAsync(ct), count, pageNumber, pageSize);
    }

    private static async Task<(IQueryable<T> itemsQuery, int pageNumber, int pageSize, int count)> GetPagedResultAsync<T>(IQueryable<T> query, QueryModel queryModel, CancellationToken ct) where T : class
    {
        const int MAX_PAGE_SIZE = 100;
        const int DEFAULT_PAGE_SIZE = 20;

        int pageNumber = queryModel?.Page ?? 1;
        if (pageNumber <= 0) pageNumber = 1;
        var pageSize = queryModel?.PageSize ?? DEFAULT_PAGE_SIZE;

        if (queryModel != null)
        {
            var queryKitConfig = new CustomQueryKitConfiguration();
            var queryKitData = new QueryKitData()
            {
                Filters = queryModel.Filters,
                SortOrder = queryModel.Sorts,
                Configuration = queryKitConfig
            };
            query = query.ApplyQueryKit(queryKitData);
        }

        var count = await query.CountAsync(ct);

        var skip = (pageNumber - 1) * pageSize;
        var itemsQuery = query.Skip(skip).Take(Math.Min(pageSize, MAX_PAGE_SIZE));

        return (itemsQuery, pageNumber, pageSize, count);
    }

    public static IQueryable<T> ApplyQueryResult<T>(IQueryable<T> query, QueryModel queryModel) where T : class
    {
        var queryKitConfig = new CustomQueryKitConfiguration();
        var queryKitData = new QueryKitData()
        {
            Filters = queryModel.Filters,
            SortOrder = queryModel.Sorts,
            Configuration = queryKitConfig
        };
        query = query.ApplyQueryKit(queryKitData);
        return query;
    }

    public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> source, QueryModel queryModel) where T : class
    {
        source = ApplyQueryResult(source, queryModel);
        return source;
    }
}
