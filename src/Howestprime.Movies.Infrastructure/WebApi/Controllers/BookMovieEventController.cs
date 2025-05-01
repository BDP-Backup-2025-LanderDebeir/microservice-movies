using Domaincrafters.Application;
using Howestprime.Movies.Application.MovieEvents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record BookMovieEventBody
{
    [Required]
    public required int StandardVisitors { get; set; }

    [Required]
    public required int DiscountVisitors { get; set; }
}

public sealed record BookMovieEventParameters
{
    [Required]
    public required Guid MovieEventId { get; set; }
}

public sealed class BookMovieEventController
{
    public static async Task<Results<Created, BadRequest>> Invoke(
        [AsParameters] BookMovieEventParameters parameters,
        [FromBody] BookMovieEventBody body,
        [FromServices] IUseCase<BookMovieEventInput, Task<string>> bookMovieEvent)
    {
        string movieEventId = parameters.MovieEventId.ToString();

        BookMovieEventInput input = new(
            movieEventId,
            body.StandardVisitors,
            body.DiscountVisitors
        );

        string bookingId = await bookMovieEvent.Execute(input);
        return TypedResults.Created($"/movie-events/{movieEventId}/bookings/{bookingId}");
    }
}
