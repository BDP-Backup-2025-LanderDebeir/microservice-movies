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
        return await _query.Fetch(input.MovieId) ?? throw new NotFoundException($"Movie with id {input.MovieId} not found");
    }
}
