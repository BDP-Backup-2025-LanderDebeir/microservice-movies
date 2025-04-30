using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.MovieEvents;
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
    public static async Task<Results<Ok<MovieWithEventsDTO>, NotFound>> Invoke(
        [AsParameters] FindMovieByIdWithEventsParameters parameters,
        [FromServices] IUseCase<FindMovieByIdWithEventsInput, MovieData> findMovieByIdWithEvents
        )
    {
        FindMovieByIdWithEventsInput input = new(parameters.MovieId.ToString());
        MovieData movie = findMovieByIdWithEvents.Execute(input);
        return TypedResults.Ok(BuildResponse(movie));
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
