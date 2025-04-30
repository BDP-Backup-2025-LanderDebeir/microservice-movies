using Domaincrafters.Domain;

namespace Howestprime.Movies.Domain.Movie;

public sealed class MovieId(string? id = "") : UuidEntityId(id);

public class Movie : Entity<MovieId>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Year { get; private set; }
    public int Duration { get; private set; }
    public string Genres { get; private set; }
    public string Actors { get; private set; }
    public int AgeRating { get; private set; }
    public string PosterUrl { get; private set; }

    private Movie(MovieId id, string title, string description, int year, int duration, string genres, string actors, int ageRating, string posterUrl): base(id)
    {
        Title = title;
        Description = description;
        Year = year;
        Duration = duration;
        Genres = genres;
        Actors = actors;
        AgeRating = ageRating;
        PosterUrl = posterUrl;
    }


    public static Movie Create(string title, string description, int year, int duration, string genres, string actors, int ageRating, string posterUrl, MovieId? id = null)
    {
        id ??= new MovieId();

        Movie movie = new Movie(id, title, description, year, duration, genres, actors, ageRating, posterUrl);

        movie.ValidateState();

        DomainEventPublisher
            .Instance
            .Publish(MovieRegistered.Create(id.Value, title, description, year, duration, genres, actors, ageRating, posterUrl));

        return movie;
    }
    public override void ValidateState()
    {
        EnsureValidTitle(Title);
        EnsureValidYear(Year);
        EnsureValidDuration(Duration);
        EnsureValidGenres(Genres);
        EnsureValidActors(Actors);
        EnsureValidDescription(Description);
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
        if (int.IsNegative(year) || year > DateTime.Now.Year)
        {
            throw new ArgumentException("Release year can not be negative or in the future");
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
    private static void EnsureValidDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description can not be empty");
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

    private Movie() : base(new MovieId()) { }
}