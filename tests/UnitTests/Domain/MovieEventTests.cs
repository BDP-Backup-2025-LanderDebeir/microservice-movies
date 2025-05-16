using Howestprime.Movies.Domain.Movie;
using Howestprime.Movies.Domain.MovieEvent;

namespace UnitTests.Domain;

public class MovieEventTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateMovieEvent()
    {
        //Arrange + Act
        MovieEventId movieEventId = new();
        MovieId movieId = new();
        RoomId roomId = new();
        DateTime time = new(2099, 12, 31, 15, 0, 0);

        MovieEvent movieEvent = MovieEvent.Create(movieId, roomId, time, 100, movieEventId);

        //Assert
        Assert.NotNull(movieEvent);
        Assert.Equal(movieEventId, movieEvent.Id);
        Assert.Equal(movieId, movieEvent.MovieId);
        Assert.Equal(roomId, movieEvent.RoomId);
        Assert.Equal(time, movieEvent.Time);
        Assert.Equal(100, movieEvent.Capacity);
        Assert.Equal(0, movieEvent.Visitors);
    }

    [Fact]
    public void Create_WithInvalidCapacity_ShouldThrowError()
    {
        //Arrange
        MovieEventId movieEventId = new();
        MovieId movieId = new();
        RoomId roomId = new();
        DateTime time = new(2099, 12, 31, 15, 0, 0);

        //Act + Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Create(movieId, roomId, time, -1, movieEventId));
    }

    [Fact]
    public void Create_WithDateInThePast_ShouldThrowError()
    {
        //Arrange
        MovieEventId movieEventId = new();
        MovieId movieId = new();
        RoomId roomId = new();
        DateTime time = new(1984, 12, 31, 15, 0, 0);

        //Act + Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Create(movieId, roomId, time, 100, movieEventId));
    }

    [Fact]
    public void Create_WithInvalidTime_ShouldThrowError()
    {
        //Arrange
        MovieEventId movieEventId = new();
        MovieId movieId = new();
        RoomId roomId = new();
        DateTime time = new(2099, 12, 31, 16, 0, 0);

        //Act + Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Create(movieId, roomId, time, 100, movieEventId));
    }

    [Fact]
    public void Book_WithValidBooking_ShouldAddToBookings()
    {
        //Arrange
        MovieEventId movieEventId = new();
        MovieEvent movieEvent = MovieEvent.Create(new(), new(), new(2099, 12, 31, 15, 0, 0), 100, movieEventId);
        Booking booking = Booking.Create(movieEventId, 1, 1);
        Room room = Room.Create("Velvet room", 100);

        //Act
        movieEvent.Book(booking, room);

        //Assert
        Assert.Single(movieEvent.Bookings);
        var result = movieEvent.Bookings[0];

        Assert.Equal(booking, result);
        Assert.NotEmpty(result.SeatNumbers);
        Assert.Equal(2, movieEvent.Visitors);
    }

    [Fact]
    public void Book_WhenItsFull_ShouldThrowError()
    {
        //Arrange
        MovieEventId movieEventId = new();
        MovieEvent movieEvent = MovieEvent.Create(new(), new(), new(2099, 12, 31, 15, 0, 0), 1, movieEventId);
        Booking booking = Booking.Create(movieEventId, 1, 1);
        Room room = Room.Create("Velvet room", 100);

        //Act + Assert
        Assert.Throws<ArgumentException>(() => movieEvent.Book(booking, room));
    }
}
