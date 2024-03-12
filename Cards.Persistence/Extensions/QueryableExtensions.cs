using Cards.Domain.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Cards.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            PagedList<T> pagedList = new PagedList<T>
            {
                Items = items,
                MetaData = new MetaData
                {
                    TotalCount = count,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(count / (double)pageSize)
                }
            };
            return pagedList;
        }
    }
}
