using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record FindMovieEventsForMonthInput(
    int Month,
    int Year
    );

public class FindMovieEventsForMonth(
    IAllMovieEventsQuery query
    ) : IUseCase<FindMovieEventsForMonthInput, Task<IReadOnlyList<MovieEventData>>>
{
    private readonly IAllMovieEventsQuery _query = query;
    public async Task<IReadOnlyList<MovieEventData>> Execute(FindMovieEventsForMonthInput input)
    {
        if (input.Month <= 1 || input.Month >= 12 || input.Year < DateTime.Now.Year)
            throw new InvalidOperationException("Invalid input data");

        return await _query.Fetch(MovieEventDataExpressions.EventsInMonthAndYear(input.Month, input.Year));
    }
}
