using Azure;
using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record FindMovieEventsForMonthParameters
{
    [Required]
    public required int Month { get; set; }
    [Required]
    public required int Year { get; set; }
}

public sealed class FindMovieEventsForMonthController
{
    public static async Task<Results<Ok<MovieEventsDTO>, BadRequest<string>> Invoke(
        [AsParameters] FindMovieEventsForMonthParameters parameters,
        [FromServices] IUseCase<FindMovieEventsForMonthInput, Task<IReadOnlyList<MovieEventData>>> findMovieEventsForMonth
        )
    {
        try
        {
            FindMovieEventsForMonthInput input = new(parameters.Month, parameters.Year);
            IReadOnlyList<MovieEventData> events = await findMovieEventsForMonth.Execute(input);
            return TypedResults.Ok(BuildResponse(events));
        }
        catch(InvalidOperationException e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    public static MovieEventsDTO BuildResponse(IReadOnlyList<MovieEventData> events)
    {
        return new MovieEventsDTO(
            events.Select(e => new MovieEventDetailDTO(
                new Guid(e.RoomId),
                new RoomDto(
                    new Guid(e.Room.Id),
                    e.Room.Name,
                    e.Room.Capacity
                    ),
                e.Time,
                new MovieDTO(
                    new Guid(e.MovieId),
                    e.Movie.Title,
                    e.Movie.Genre,
                    e.Movie.Description,
                    e.Movie.Year,
                    e.Movie.Duration,
                    e.Movie.Actors,
                    e.Movie.AgeRating,
                    e.Movie.PosterUrl
                    ),
                e.Capacity
                )));
    }
}
