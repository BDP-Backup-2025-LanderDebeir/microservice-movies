using System.Linq.Expressions;

namespace Howestprime.Movies.Application.Contracts.Data.Filters;

public static class MovieDataExpressions
{
    public static Expression<Func<MovieData, bool>> TitleAndGenreContains(string title, string genre)
    {
        return movieData => movieData.Title.ToLower().Contains(title.ToLower()) && movieData.Genre.ToLower().Contains(genre.ToLower());
    }
}