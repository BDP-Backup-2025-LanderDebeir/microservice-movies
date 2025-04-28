using Domaincrafters.Domain;
using Howestprime.Movies.Domain.Movie;

namespace Howestprime.Movies.Domain.MovieEvent;

public sealed class MovieEventId(string? id = ""): UuidEntityId(id);

public class MovieEvent : Entity<MovieEventId>
{
    public MovieId MovieId { get; private set; }
    public RoomId RoomId { get; private set; }
    public DateTime Time { get; private set; }
    public int Visitors { get; private set; }
    public int Capacity { get; private set; }
    public IReadOnlyList<Booking> Bookings { get; private set; }

    private MovieEvent(MovieEventId id, MovieId movieId, RoomId roomId, DateTime time, int visitors, int capacity) : base(id)
    {
        MovieId = movieId;
        RoomId = roomId;
        Time = time;
        Visitors = visitors;
        Capacity = capacity;
        Bookings = new List<Booking>();
    }

    public static MovieEvent Create(MovieId movieId, RoomId roomId, DateTime time, int visitors, int capacity, MovieEventId? id = null)
    {
        id ??= new MovieEventId();

        MovieEvent movieEvent = new MovieEvent(id, movieId, roomId, time, visitors, capacity);

        movieEvent.ValidateState();

        return movieEvent;
    }


    public override void ValidateState()
    {
        EnsureValidVisitors(Visitors);
        EnsureValidCapacity(Capacity);
    }

    public static void EnsureValidVisitors(int visitors)
    {
        if (int.IsNegative(visitors))
        {
            throw new ArgumentException("Amount of visitors has to be positive");
        }
    }

    public static void EnsureValidCapacity(int capacity)
    {
        if (int.IsNegative(capacity))
        {
            throw new ArgumentException("Capacity has to be positive");
        }
    }
}
