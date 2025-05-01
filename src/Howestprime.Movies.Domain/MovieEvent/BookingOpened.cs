using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent;

public sealed class BookingOpened : HowestprimeDomainEvent
{
    public string BookingId { get; init; }
    public string MovieId { get; init; }
    public string Room { get; init; }
    public DateTime Time { get; init; }
    public int StandardVisitors { get; init; }
    public List<int> SeatNumbers { get; init; }
    private BookingOpened(string bookingId, string movieId, string room, DateTime time, int standardVisitors, List<int> seatNumbers) : base(nameof(BookingOpened))
    {
        BookingId = bookingId;
        MovieId = movieId;
        Room = room;
        Time = time;
        StandardVisitors = standardVisitors;
        SeatNumbers = seatNumbers;
    }

    public static BookingOpened Create(string bookingId, string movieId, string room, DateTime time, int standardVisitors, List<int> seatNumbers)
    {
        return new BookingOpened(bookingId, movieId, room, time, standardVisitors, seatNumbers);
    }
}
