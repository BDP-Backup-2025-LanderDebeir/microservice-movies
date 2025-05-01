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
    public List<Booking> Bookings { get; private set; }

    private MovieEvent(MovieEventId id, MovieId movieId, RoomId roomId, DateTime time, int capacity) : base(id)
    {
        MovieId = movieId;
        RoomId = roomId;
        Time = time;
        Visitors = 0;
        Capacity = capacity;
        Bookings = [];
    }

    public static MovieEvent Create(MovieId movieId, RoomId roomId, DateTime time, int capacity, MovieEventId? id = null)
    {
        id ??= new MovieEventId();

        MovieEvent movieEvent = new MovieEvent(id, movieId, roomId, time, capacity);

        movieEvent.ValidateState();

        return movieEvent;
    }


    public override void ValidateState()
    {
        EnsureValidCapacity(Capacity);
        EnsureDayInFuture(Time);
        EnsureValidTime(Time);
    }

    public static void EnsureValidCapacity(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity has to be positive");
        }
    }

    public static void EnsureDayInFuture(DateTime time)
    {
        if (time.Date.CompareTo(DateTime.Now.Date) != 1)
            throw new ArgumentException("Event has to be planned in the future");
    }

    public static void EnsureValidTime(DateTime time)
    {
        if (!((time.Hour == 15 && time.Minute == 0 && time.Second == 0) || (time.Hour == 19 && time.Minute == 0 && time.Second == 0)))
            throw new ArgumentException("Event has to at either 15:00 or 19:00");
    }

    public void Book(Booking booking)
    {
        if (Visitors + booking.StandardVisitors + booking.DiscountVisitors > Capacity)
            throw new ArgumentException("Too many visitors");

        for (int i = 0; i < booking.StandardVisitors + booking.DiscountVisitors; i++)
        {
            Visitors++;
            booking.SeatNumbers.Add(Visitors);
        }

        Bookings.Add(booking);
    }
}

