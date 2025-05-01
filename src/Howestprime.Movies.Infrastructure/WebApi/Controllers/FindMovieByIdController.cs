using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Domain.Shared.Exceptions;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record FindMovieByIdParameters
{
    [Required]
    public required Guid Id { get; set; }
}

public sealed class FindMovieByIdController
{
    public static async Task<Results<Ok<MovieDTO>, NotFound<string>>> Invoke(
        [AsParameters] FindMovieByIdParameters parameters,
        [FromServices] IUseCase<FindMovieByIdInput, Task<MovieData>> findMovieById
        )
    {
        FindMovieByIdInput input = new(parameters.Id.ToString());
        try
        {
            MovieData movie = await findMovieById.Execute(input);

            return TypedResults.Ok(BuildResponse(movie));
        }
        catch(NotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }

    private static MovieDTO BuildResponse(MovieData movie)
    {
        return new MovieDTO(
            new Guid(movie.Id),
            movie.Title,
            movie.Genre,
            movie.Description,
            movie.Year,
            movie.Duration,
            movie.Actors,
            movie.AgeRating,
            movie.PosterUrl
            );


    }
}