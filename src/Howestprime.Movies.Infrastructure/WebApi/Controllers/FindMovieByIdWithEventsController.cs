using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.Shared.Exceptions;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record FindMovieByIdWithEventsParameters
{
    [Required]
    public required Guid MovieId { get; set; }
}

public sealed class FindMovieByIdWithEventsController
{
    public static async Task<Results<Ok<MovieWithEventsDTO>, NotFound<string>>> Invoke(
        [AsParameters] FindMovieByIdWithEventsParameters parameters,
        [FromServices] IUseCase<FindMovieByIdWithEventsInput, Task<MovieData>> findMovieByIdWithEvents
        )
    {
        try
        {
            FindMovieByIdWithEventsInput input = new(parameters.MovieId.ToString());
            MovieData movie = await findMovieByIdWithEvents.Execute(input);
            return TypedResults.Ok(BuildResponse(movie));
        }
        catch(NotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }

    private static MovieWithEventsDTO BuildResponse(MovieData movie)
    {
        return new(
            new Guid(movie.Id),
            movie.Title,
            movie.Genre,
            movie.Description,
            movie.Year,
            movie.Duration,
            movie.Actors,
            movie.AgeRating,
            movie.PosterUrl,
            movie.Events.Select(e => new MovieEventDto(
                                        new Guid(e.Id),
                                        new RoomDto(new Guid(e.Room.Id), e.Room.Name, e.Room.Capacity),
                                        e.Time,
                                        new Guid(e.MovieId))
            ).ToList()
            );
    }
}
