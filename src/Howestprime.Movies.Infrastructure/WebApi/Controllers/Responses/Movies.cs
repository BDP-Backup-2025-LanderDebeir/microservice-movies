namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record Movies(
    IEnumerable<Movie> Data
    );
