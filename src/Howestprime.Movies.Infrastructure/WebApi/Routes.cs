using Howestprime.Movies.Infrastructure.WebApi.Controllers;
using Howestprime.Movies.Infrastructure.WebApi.Shared.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net.Mime;

namespace Howestprime.Movies.Infrastructure.WebApi;

public static class Routes
{
    public static OpenApiInfo OpenApiInfo { get; } = new OpenApiInfo
    {
        Version = "v1",
        Title = "Movies API",
        Description = "A simple API to manage movies and movie events.",
        Contact = new OpenApiContact
        {
            Name = "Lander Debeir",
            Email = "lander.debeir@student.howest.be"
        }
    };

    public static WebApplication MapRoutes(this WebApplication app)
    {
        MapMovieRoutes(app);
        MapMovieEventsRoutes(app);
        return app;
    }

    private static void MapMovieRoutes(WebApplication app)
    {
        var movieGroup = app.MapGroup("/api/movies")
            .WithTags("Movies")
            .WithOpenApi();

        movieGroup.MapPost("/", RegisterMovieController.Invoke)
             .WithName("RegisterMovie")
             .WithDescription("Registers a new movie")
             .WithMetadata(new ConsumesAttribute(MediaTypeNames.Application.Json))
             .AddEndpointFilter<BodyValidatorFilter<RegisterMovieBody>>()
             .WithOpenApi();

        movieGroup.MapGet("/", FindMovieByFiltersController.Invoke)
            .WithName("FindMovieByFilters")
            .WithDescription("Lists movies by Title & genre")
            .WithMetadata(new ProducesAttribute(MediaTypeNames.Application.Json))
            .WithOpenApi();

        movieGroup.MapGet("/{id}", FindMovieByIdController.Invoke)
            .WithName("FindMovieById")
            .WithDescription("Find a movie by id")
            .WithMetadata(new ProducesAttribute(MediaTypeNames.Application.Json))
            .WithOpenApi();

    }

    private static void MapMovieEventsRoutes(WebApplication app)
    {
        var movieEventGroup = app.MapGroup("/api/movie-events")
            .WithTags("Movie Events")
            .WithOpenApi();

        movieEventGroup.MapPost("/", ScheduleMovieEventController.Invoke)
            .WithName("ScheduleMovieEvent")
            .WithDescription("Schedule a movie event")
            .WithMetadata(new ConsumesAttribute(MediaTypeNames.Application.Json))
            .AddEndpointFilter<BodyValidatorFilter<ScheduleMovieEventBody>>()
            .WithOpenApi();

        movieEventGroup.MapGet("/movie", FindMovieByIdWithEventsController.Invoke)
            .WithName("FindMovieByIdWithEvent")
            .WithDescription("List all movie events for a specific movie")
            .WithMetadata(new ProducesAttribute(MediaTypeNames.Application.Json))
            .WithOpenApi();

        movieEventGroup.MapGet("/monthly", FindMovieEventsForMonthController.Invoke)
            .WithName("FindMovieEventsForMonth")
            .WithDescription("List all movie events for a specific month.")
            .WithMetadata(new ProducesAttribute(MediaTypeNames.Application.Json))
            .WithOpenApi();

        movieEventGroup.MapGet("/", FindMovieWithEventsInTimeFrameController.Invoke)
            .WithName("FindMovieWithEventsInTimeFrame")
            .WithDescription("List all movie events per movie in the next two weeks")
            .WithMetadata(new ProducesAttribute(MediaTypeNames.Application.Json))
            .WithOpenApi();

        movieEventGroup.MapPost("/{movieEventId}/bookings", BookMovieEventController.Invoke)
            .WithName("BookMovieEvent")
            .WithDescription("Book a movie Event")
            .WithMetadata(new ConsumesAttribute(MediaTypeNames.Application.Json))
            .AddEndpointFilter<BodyValidatorFilter<BookMovieEventBody>>()
            .WithOpenApi();
    }
}
