using Domaincrafters.Domain;

namespace Howestprime.Movies.Domain.Movie;

public sealed class MovieId(string? id = "") : UuidEntityId(id);

public class Movie : Entity<MovieId>
{
    public string Title { get; private set; }
    public int Year { get; private set; }
    public int Duration { get; private set; }
    public string Genres { get; private set; }
    public string Actors { get; private set; }
    public string Directors { get; private set; }
    public int AgeRating { get; private set; }
    public string PosterUrl { get; private set; }

    private Movie(MovieId id, string title, int year, int duration, string genres, string actors, string directors, int ageRating, string posterUrl): base(id)
    {
        Title = title;
        Year = year;
        Duration = duration;
        Genres = genres;
        Actors = actors;
        Directors = directors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }


    public static Movie Create(string title, int year, int duration, string genres, string actors, string directors, int ageRating, string posterUrl, MovieId? id = null)
    {
        id ??= new MovieId();

        Movie movie = new Movie(id, title, year, duration, genres, actors, directors, ageRating, posterUrl);

        movie.ValidateState();

        return movie;
    }
    public override void ValidateState()
    {
        EnsureValidTitle(Title);
        EnsureValidYear(Year);
        EnsureValidDuration(Duration);
        EnsureValidGenres(Genres);
        EnsureValidActors(Actors);
        EnsureValidDirectors(Directors);
        EnsureValidAgeRating(AgeRating);
        EnsureValidPosterUrl(PosterUrl);
    }

    private static void EnsureValidTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Movie title can not be empty");
        }
    }

    private static void EnsureValidYear(int year)
    {
        if (int.IsNegative(year))
        {
            throw new ArgumentException("Release year can not be negative");
        }
    }

    private static void EnsureValidDuration(int duration)
    {
        if (int.IsNegative(duration))
        {
            throw new ArgumentException("Movie duration can not be negative");
        }
    }
    
    private static void EnsureValidGenres(string genres)
    {
        if (string.IsNullOrWhiteSpace(genres))
        {
            throw new ArgumentException("Genres can not be empty");
        }
    }

    private static void EnsureValidActors(string actors)
    {
        if (string.IsNullOrWhiteSpace(actors))
        {
            throw new ArgumentException("Actors can not be empty");
        }
    }
    private static void EnsureValidDirectors(string directors)
    {
        if (string.IsNullOrWhiteSpace(directors))
        {
            throw new ArgumentException("Directors can not be empty");
        }
    }

    private static void EnsureValidAgeRating(int ageRating)
    {
        if (int.IsNegative(ageRating))
        {
            throw new ArgumentException("Directors can not be empty");
        }
    }

    private static void EnsureValidPosterUrl(string posterUrl)
    {
        if (string.IsNullOrWhiteSpace(posterUrl))
        {
            throw new ArgumentException("Poster url can not be empty");
        }
    }
}