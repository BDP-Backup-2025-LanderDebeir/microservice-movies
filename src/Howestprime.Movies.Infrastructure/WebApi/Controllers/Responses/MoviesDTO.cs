namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record MoviesDTO(
    IEnumerable<MovieDTO> Data
    );
