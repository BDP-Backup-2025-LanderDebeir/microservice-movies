using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.Movies;

public sealed record FindMovieByFilterInput(
    string? Title,
    string? Genre
    );

public class FindMovieByFilter(
    IAllMoviesQuery query
    ) : IUseCase<FindMovieByFilterInput, Task<IReadOnlyList<MovieData>>>
{
    private IAllMoviesQuery _query = query;

    public async Task<IReadOnlyList<MovieData>> Execute(FindMovieByFilterInput input)
    {
        return await _query.Fetch(MovieDataExpressions.TitleAndGenreContains(input.Title ?? "", input.Genre ?? ""));
    }
}