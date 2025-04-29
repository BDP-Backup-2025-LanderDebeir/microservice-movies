namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses
{
    public sealed record Movie(
        string Id,
        string Title,
        string Genre,
        string Description,
        int Year,
        int Duration,
        string Actors,
        int AgeRating,
        string PosterUrl
        );
}
