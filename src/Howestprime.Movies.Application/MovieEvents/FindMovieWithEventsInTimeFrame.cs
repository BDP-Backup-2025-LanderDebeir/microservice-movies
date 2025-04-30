using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record FindMovieWithEventsInTimeFrameInput(
    string? Title,
    string? Genre
    );

public class FindMovieWithEventsInTimeFrame(
    IAllMoviesWithEventsQuery query
    ) : IUseCase<FindMovieWithEventsInTimeFrameInput, Task<IReadOnlyList<MovieData>>>
{
    private readonly IAllMoviesWithEventsQuery _query = query;

    public async Task<IReadOnlyList<MovieData>> Execute(FindMovieWithEventsInTimeFrameInput input)
    {
        List<MovieData> movies = [.. await _query.Fetch(MovieDataExpressions.TitleAndGenreContains(input.Title ?? "", input.Genre ?? ""))];
        movies.ForEach(m => m.Events = m.Events.Where(e => (e.Time.Year - DateTime.Now.Year == 0 || e.Time.Year - DateTime.Now.Year == 1) && e.Time.DayOfYear - DateTime.Now.DayOfYear <= 14).ToList());
        return movies;
    }
}
