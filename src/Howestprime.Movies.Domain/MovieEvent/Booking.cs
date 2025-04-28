using Domaincrafters.Domain;

namespace Howestprime.Movies.Domain.MovieEvent;

public sealed class BookingId(string? id = ""): UuidEntityId(id);

public enum BookingStatus
{
    OPEN,
    CLOSED
}

public enum PaymentStatus
{
    PENDING,
    SUCCESS,
    FAILED
}

public class Booking : Entity<BookingId>
    
{
    public MovieEventId MovieEventId { get; private set; }
    public BookingStatus BookingStatus { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public int StandardVisitors { get; private set; }
    public int DiscountVisitors { get; private set; }
    public IReadOnlyList<int> SeatNumbers { get; private set; }
    private Booking(BookingId id, MovieEventId movieEventId, BookingStatus bookingStatus, PaymentStatus paymentStatus, int standardVisitors, int discountVisitors, List<int> seatNumbers): base(id)
    {
        MovieEventId = movieEventId;
        BookingStatus = bookingStatus;
        PaymentStatus = paymentStatus;
        StandardVisitors = standardVisitors;
        DiscountVisitors = discountVisitors;
        SeatNumbers = seatNumbers;
    }

    public static Booking Create(MovieEventId movieEventId, BookingStatus bookingStatus, PaymentStatus paymentStatus, int standardVisitors, int discountVisitors, List<int> seatNumbers, BookingId? id = null)
    {
        id ??= new BookingId();
        Booking booking = new Booking(id, movieEventId, bookingStatus, paymentStatus, standardVisitors, discountVisitors, seatNumbers);

        booking.ValidateState();

        return booking;
    }

    public override void ValidateState()
    {
        EnsureValidStandardVisitors(StandardVisitors);
        EnsureValidDiscountVisitors(DiscountVisitors);
    }

    private static void EnsureValidStandardVisitors(int standardVisitors)
    {
        if (int.IsNegative(standardVisitors))
        {
            throw new ArgumentException("Standard visitors can not be empty");
        }
    }
    private static void EnsureValidDiscountVisitors(int discountVisitors)
    {
        if (int.IsNegative(discountVisitors))
        {
            throw new ArgumentException("Discount visitors can not be empty");
        }
    }
}