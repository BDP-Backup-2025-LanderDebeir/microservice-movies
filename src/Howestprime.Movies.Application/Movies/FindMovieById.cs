using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using System.Threading.Tasks;

namespace Howestprime.Movies.Application.Movies;

public sealed record FindMovieByIdInput(
    string Id
    );

public class FindMovieById(
    IFindMovieByIdQuery query
    ) : IUseCase<FindMovieByIdInput, Task<MovieData>>
{
    private readonly IFindMovieByIdQuery _query = query;

    public async Task<MovieData> Execute(FindMovieByIdInput input)
    {
        return await _query.Fetch(input.Id) ?? throw new InvalidOperationException($"Movie with id ${input.Id} not found");
    }
}