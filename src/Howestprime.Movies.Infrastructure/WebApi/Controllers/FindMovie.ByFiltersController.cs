using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record FindMovieByFiltersParameters
{
    public string? Title { get; init; }
    public string? Genre { get; init; }
}

public sealed class FindMovieByFiltersController
{
    public static async Task<Results<Ok<MoviesDTO>>> Invoke(
        [AsParameters] FindMovieByFiltersParameters parameters,
        [FromServices] IUseCase<FindMovieByFilterInput, Task<IReadOnlyList<MovieData>>> findMovieByFilter
        )
    {
        FindMovieByFilterInput input = new(
            parameters.Title,
            parameters.Genre
            );

        IReadOnlyList<MovieData> movies = await findMovieByFilter.Execute(input);

        return TypedResults.Ok(BuildResponse(movies));
    }

    private static MoviesDTO BuildResponse(IReadOnlyList<MovieData> movies)
    {
        return new MoviesDTO(
            movies.Select(movie => new MovieDTO(
                movie.Id,
                movie.Title,
                movie.Genre,
                movie.Description,
                movie.Year,
                movie.Duration,
                movie.Actors,
                movie.AgeRating,
                movie.PosterUrl
                ))
            );
    }
}
