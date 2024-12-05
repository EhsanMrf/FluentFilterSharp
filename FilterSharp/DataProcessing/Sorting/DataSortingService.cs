using System.Linq.Expressions;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.Sorting;

public class DataSortingService :IDataSortingService
{
    public  IQueryable<T> ApplyOrderByMultiple<T>(IQueryable<T> query, List<SortingRequest>? orderByClauses)
    {
        if (orderByClauses == null || !orderByClauses.Any())
            return query;

        IOrderedQueryable<T>? orderedQuery = null;

        for (var i = 0; i < orderByClauses.Count; i++)
        {
            var clause = orderByClauses[i];

            // Validate property name
            var entityType = typeof(T);
            var property = entityType.GetProperty(clause.FiledName);
            if (property == null)
                throw new ArgumentException($"Property '{clause.FiledName}' does not exist on type '{entityType.Name}'");

            // Create parameter and lambda (x => x.Property)
            var parameter = Expression.Parameter(entityType, "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var lambda = Expression.Lambda(propertyAccess, parameter);

            // Determine the method name
            var methodName = clause.Ascending
                ? (i == 0 ? "OrderBy" : "ThenBy")
                : (i == 0 ? "OrderByDescending" : "ThenByDescending");

            // Create method call dynamically
            var methodCall = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { entityType, property.PropertyType },
                i == 0 ? query.Expression : orderedQuery.Expression,
                Expression.Quote(lambda));

            orderedQuery = (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(methodCall);
        }
        return orderedQuery!;
    }
}