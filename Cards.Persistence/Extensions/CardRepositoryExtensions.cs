using Cards.Domain.Entities;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Cards.Persistence.Extensions.Utility;

namespace Cards.Persistence.Extensions
{
    public static class CardRepositoryExtensions
    {
        public static IQueryable<Card> FilterByCardQueryFilters(this IQueryable<Card> cardQuery,
            Dictionary<string, object> cardQueryFilters)
        {
            foreach (var key in cardQueryFilters)
            {
                switch (key.Key)
                {
                    case "Name":
                        cardQuery = cardQuery.Where(card => card.Name == (string)key.Value);
                        break;
                    case "Color":
                        cardQuery = cardQuery.Where(card => card.Color == (string)key.Value);
                        break;
                    case "StatusId":
                        cardQuery = cardQuery.Where(card => card.StatusId == (string)key.Value);
                        break;
                    case "DateOfCreation":
                        //cardQuery = cardQuery.Where(card => card.DateOfCreation == (DateTime)key.Value);
                        DateTime selectedDate = ((DateTime)key.Value).Date;
                        DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 0, 0, 0, DateTimeKind.Utc);
                        DateTime endDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 23, 59, 59, DateTimeKind.Utc);
                        cardQuery = cardQuery.Where(card => card.DateOfCreation >= startDate && card.DateOfCreation <= endDate);
                        break;
                    default:
                        break;
                }
            }
            return cardQuery;
        }

        public static IQueryable<Card> Sort(this IQueryable<Card> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Card>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Name);

            return employees.OrderBy(orderQuery);
        }


    }
}
