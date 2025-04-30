using System.Linq.Expressions;

namespace Howestprime.Movies.Application.Contracts.Data.Filters;

public static class MovieEventDataExpressions
{
    public static Expression<Func<MovieEventData, bool>> EventsInMonthAndYear(int month, int year)
    {
        return movieEventData => movieEventData.Time.Year == year && movieEventData.Time.Month == month;
    }
}
