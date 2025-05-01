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

public sealed class BookMovieEventController
{
    public static async Task<Results<Created, BadRequest>> Invoke(
        [FromBody] BookMovieEventBody body,
        [AsParameters] Guid movieEventId,
        [FromServices] IUseCase<BookMovieEventInput, Task<string>> bookMovieEvent)
    {
        BookMovieEventInput input = new(
            movieEventId.ToString(),
            body.StandardVisitors,
            body.DiscountVisitors
        );

        string bookingId = await bookMovieEvent.Execute(input);
        return TypedResults.Created($"/movie-events/{movieEventId}/bookings/{bookingId}");
    }
}
