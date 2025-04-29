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
            Name = "Matthias Blomme",
            Email = "matthias.blomme@howest.be"
        }
    };

    public static WebApplication MapRoutes(this WebApplication app)
    {
        MapMovieRoutes(app);
        return app;
    }

    private static void MapMovieRoutes(WebApplication app)
    {
        var movieGroup = app.MapGroup("/api/movies")
            .WithTags("Movies")
            .WithDescription("Endpoints related to movie registration")
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

    }
}
