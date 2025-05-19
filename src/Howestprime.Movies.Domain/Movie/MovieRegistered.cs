using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.Movie;

public sealed class MovieRegistered : HowestprimeDomainEvent
{
    public string MovieId { get; init; }
    public string Title { get; init;}
    public string Description { get; init; }
    public int Year { get; init; }
    public int Duration { get; init; }
    public List<string> Genres { get; init; }
    public int AgeRating { get; init; }
    public string PosterUrl { get; init; }

    private MovieRegistered(string movieId, string title, string description, int year, int duration, List<string> genres, int ageRating, string posterUrl) : base(nameof(MovieRegistered))
    {
        MovieId = movieId;
        Title = title;
        Description = description;
        Year = year;
        Duration = duration;
        Genres = genres;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }

    public static MovieRegistered Create(string movieId, string title, string description, int year, int duration, List<string> genres, int ageRating, string posterUrl)
    {
        return new MovieRegistered(movieId, title, description, year, duration, genres, ageRating, posterUrl);
    }
}