using Cards.Domain.Entities;

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
                        cardQuery = cardQuery.Where(card => card.StatusId == (int)key.Value);
                        break;
                    case "StartDate":
                        cardQuery = cardQuery.Where(card => card.DateOfCreation >= (DateTime)key.Value);
                        break;
                    case "EndDate":
                        cardQuery = cardQuery.Where(card => card.DateOfCreation <= (DateTime)key.Value);
                        break;
                    default:
                        break;
                }
            }
            return cardQuery;
        }

       

        public static IQueryable<Card> Search(this IQueryable<Card> cards, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return cards;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return cards.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

     /*   public static IQueryable<Card> Sort(this IQueryable<Card> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(e => e.Name);
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Card).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
               pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Name);
            return employees.OrderBy(orderQuery);
        }*/


    }
}
