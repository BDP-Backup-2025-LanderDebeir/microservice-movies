using Howestprime.Movies.Domain.MovieEvent;

namespace UnitTests.Domain;

public class BookingTests
{
    [Fact]
    public void Create_WithValidInformation_ShouldCreateBooking()
    {
        //Arrange + Act
        BookingId bookingId = new();
        MovieEventId movieEventId = new();

        Booking booking = Booking.Create(movieEventId, 1, 1, bookingId);

        //Assert
        Assert.Equal(bookingId, booking.Id);
        Assert.Equal(movieEventId, booking.MovieEventId);
        Assert.Equal(1, booking.StandardVisitors);
        Assert.Equal(1, booking.DiscountVisitors);
        Assert.Equal(BookingStatus.Open, booking.BookingStatus);
        Assert.Equal(PaymentStatus.Pending, booking.PaymentStatus);
        Assert.Empty(booking.SeatNumbers);
    }

    [Fact]
    public void Create_WithNegativeNumbers_ShouldThrowError()
    {
        //Arrange
        MovieEventId movieEventId = new();

        //Act + Assert
        Assert.Throws<ArgumentException>(() => Booking.Create(movieEventId, -1, -1));
    }
}
