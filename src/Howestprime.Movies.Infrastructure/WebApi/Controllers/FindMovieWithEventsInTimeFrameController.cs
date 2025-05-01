using Domaincrafters.Application;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers;

public sealed record FindMovieWithEventsInTimeFrameParameters
{
    public  string? Title { get; set; }

    public  string? Genre { get; set; }
}

public class FindMovieWithEventsInTimeFrameController
{
    public static async Task<Results<Ok<MoviesWithEventsDTO>, BadRequest>> Invoke(
        [AsParameters] FindMovieWithEventsInTimeFrameParameters parameters,
        [FromServices] IUseCase<FindMovieWithEventsInTimeFrameInput, Task<IReadOnlyList<MovieData>>> findMovieWithEventsInTimeFrame
        )
    {
        FindMovieWithEventsInTimeFrameInput input = new(parameters.Title, parameters.Genre);

        IReadOnlyList<MovieData> movies = await findMovieWithEventsInTimeFrame.Execute(input);

        return TypedResults.Ok(BuildResponse(movies));
    }

    private static MoviesWithEventsDTO BuildResponse(IReadOnlyList<MovieData> movies)
    {
        return new MoviesWithEventsDTO(
            movies.Select(m => new MovieWithEventsDTO(
                new Guid(m.Id),
                m.Title,
                m.Genre,
                m.Description,
                m.Year,
                m.Duration,
                m.Actors,
                m.AgeRating,
                m.PosterUrl,
                m.Events.Select(e => new MovieEventDto(
                    new Guid(e.Id),
                    new RoomDto(new Guid(e.RoomId), e.Room.Name, e.Room.Capacity),
                    e.Time,
                    new Guid(e.MovieId)
                    )).ToList()
                )).ToList()
            );
    }
}