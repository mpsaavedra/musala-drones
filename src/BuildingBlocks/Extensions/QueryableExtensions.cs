using System.Linq.Expressions;

namespace Musala.Drones.BuildingBlocks.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ToWhere<T>(this IQueryable<T> queryable,
        Expression<Func<T, bool>> @where = null!)
        where T : class
    {
        if (where != default)
        {
            queryable = queryable.Where(@where);
        }

        return queryable;
    }
}