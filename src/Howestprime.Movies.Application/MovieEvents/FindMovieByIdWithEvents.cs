using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Shared.Exceptions;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record FindMovieByIdWithEventsInput(
    string MovieId
    );

public class FindMovieByIdWithEvents(
    IFindMovieByIdWithEventsQuery query
    ) : IUseCase<FindMovieByIdWithEventsInput, Task<MovieData>>
{
    public IFindMovieByIdWithEventsQuery _query = query;

    public async Task<MovieData> Execute(FindMovieByIdWithEventsInput input)
    {
        MovieData movie = await _query.Fetch(input.MovieId) ?? throw new NotFoundException($"No movie with id {input.MovieId} found");

        if (movie.Events.Count <= 0)
            throw new NotFoundException($"Movie with id {input.MovieId} has no events planned within 14 days");

        return movie;
    }
}
