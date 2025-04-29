using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.ComponentModel.DataAnnotations;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record FindMovieByIdParameters
{
    [Required]
    public required string Id { get; set; }
}

public sealed class FindMovieByIdController
{
    public static async Task<Results<Ok<MovieDTO>, NotFound<string>>> Invoke(
        [AsParameters] FindMovieByIdParameters parameters,
        [FromServices] IUseCase<FindMovieByIdInput, Task<MovieData?>> findMovieById
        )
    {
        FindMovieByIdInput input = new(parameters.Id);
        MovieData? movie = await findMovieById.Execute(input);
        if (movie is null)
            return TypedResults.NotFound($"No movies with id {parameters.Id} found");

        return TypedResults.Ok(BuildResponse(movie));
    }

    private static MovieDTO BuildResponse(MovieData movie)
    {
        return new MovieDTO(
            movie.Id,
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