using Domaincrafters.Application;
using Howestprime.Movies.Application.MovieEvents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record ScheduleMovieEventBody
{
    [Required]
    public required Guid MovieId { get; set; }

    [Required]
    public required Guid RoomId { get; set; }

    [Required]
    public required DateTime StartDate { get; set; }
}

public sealed class ScheduleMovieEventController
{
    public static async Task<Results<Created, BadRequest>> Invoke(
        [FromBody] ScheduleMovieEventBody body,
        [FromServices] IUseCase<ScheduleMovieEventInput, Task<string>> scheduleMovieEvent
        )
    {
        Console.WriteLine($"Schedule movie event invoked with body: {body}");
        ScheduleMovieEventInput input = new(
            body.MovieId.ToString(),
            body.StartDate.TimeOfDay,
            body.StartDate.Date,
            body.RoomId.ToString()
            );

        string movieEventId = await scheduleMovieEvent.Execute(input);

        return TypedResults.Created($"/movie-events/{movieEventId}");
    }
}