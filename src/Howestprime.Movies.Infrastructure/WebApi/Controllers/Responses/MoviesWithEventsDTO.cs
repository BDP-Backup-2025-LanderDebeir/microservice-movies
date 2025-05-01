namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record MoviesWithEventsDTO(
    List<MovieWithEventsDTO> Data
    );
