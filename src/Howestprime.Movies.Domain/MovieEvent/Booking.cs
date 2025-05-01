using Domaincrafters.Domain;

namespace Howestprime.Movies.Domain.MovieEvent;

public sealed class BookingId(string? id = "") : UuidEntityId(id);

public enum BookingStatus
{
    Open,
    Closed
}

public enum PaymentStatus
{
    Pending,
    Succes,
    Failed
}

public class Booking : Entity<BookingId>
{
    public MovieEventId MovieEventId { get; private set; }
    public BookingStatus BookingStatus { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public int StandardVisitors { get; private set; }
    public int DiscountVisitors { get; private set; }
    public List<int> SeatNumbers { get; private set; }

    private Booking(BookingId id, MovieEventId movieEventId, int standardVisitors, int discountVisitors) : base(id)
    {
        MovieEventId = movieEventId;
        BookingStatus = BookingStatus.Open;
        PaymentStatus = PaymentStatus.Pending;
        StandardVisitors = standardVisitors;
        DiscountVisitors = discountVisitors;
        SeatNumbers = [];
    }

    public static Booking Create(MovieEventId movieEventId, int standardVisitors, int discountVisitors, BookingId? id = null)
    {
        id ??= new BookingId();

        Booking booking = new Booking(id, movieEventId, standardVisitors, discountVisitors);

        booking.ValidateState();
        return booking;
    }

    public override void ValidateState()
    {
        ensureValidVisitors(StandardVisitors + DiscountVisitors);
    }
    
    private void ensureValidVisitors(int visitors)
    {
        if (visitors <= 0)
            throw new ArgumentException("Visitors have to be bigger than 0");
    }
}