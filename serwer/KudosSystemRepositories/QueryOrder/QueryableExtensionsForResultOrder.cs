using System;
using System.Linq;
using System.Linq.Expressions;

namespace KudosSystemRepositories.QueryOrder
{
    public static class QueryableExtensionsForResultOrder
    {
        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> query,
            string orderByProperty,
            Order direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));

            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByProperty);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                direction == Order.Asc ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector)
            );

            return query.Provider.CreateQuery<T>(orderBy);
        }
    }
}
