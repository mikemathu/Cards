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
                        cardQuery = cardQuery.Where(card => card.DateOfCreation == (DateTime)key.Value);
                        break;
                    default:
                        break;
                }
            }
            return cardQuery;
        }


        public static IQueryable<Card> Sort(this IQueryable<Card> cards, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return cards.OrderBy(card => card.Name);

            string? orderQuery = OrderQueryBuilder.CreateOrderQuery<Card>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return cards.OrderBy(card => card.Name);

            IOrderedQueryable<Card> sortedCards = cards.OrderBy(orderQuery);

            return sortedCards;
        }


        public static IQueryable<Card> Search(this IQueryable<Card> cards, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return cards;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return cards.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }


    }
}
