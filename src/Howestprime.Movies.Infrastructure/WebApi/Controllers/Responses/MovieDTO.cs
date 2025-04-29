namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses
{
    public sealed record MovieDTO(
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
