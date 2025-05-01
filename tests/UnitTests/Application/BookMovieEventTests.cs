using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.MovieEvent;
using UnitTests.Shared;

namespace UnitTests.Application;

public sealed class BookMovieEventTests
{
    private readonly MockMovieEventRepository _repository;
    private readonly MockUnitOfWork _unitOfWork;
    private readonly MockTestLogger<BookMovieEvent> _logger;
    private readonly BookMovieEvent _bookMovieEvent;

    public BookMovieEventTests()
    {
        _repository = new();
        _unitOfWork = new();
        _logger = new();
        _bookMovieEvent = new(_repository, _unitOfWork, _logger);
    }

    [Fact]
    public async Task Execute_WithValidData_ShouldAddBooking()
    {
        //Arrange
        MovieEventId movieEventId = new();
        MovieEvent movieEvent = MovieEvent.Create(new(), new(), new(2025, 5, 10, 15, 0, 0), 100, movieEventId);
        await _repository.Save(movieEvent);
        BookMovieEventInput input = new(movieEventId.Value, 1, 0);

        //Act
        var result = await _bookMovieEvent.Execute(input);
        Assert.Single(movieEvent.Bookings);
        var booking = movieEvent.Bookings[0];

        Assert.Equal(result, booking.Id.Value);

        Assert.Equal(1, movieEvent.Visitors);
    }

    [Fact]
    public void Execute_WithNonExistingEvent_ShouldThrowError()
    {
        //Arrange
        BookMovieEventInput input = new(new MovieEventId().Value, 1, 0);

        //Act + Assert
        _ = Assert.ThrowsAsync<InvalidOperationException>(async () => await _bookMovieEvent.Execute(input));
    }

    [Fact]
    public async Task Execute_WithEventInTheFarFuture_ShouldThrowErrorAsync()
    {
        //Arrange
        MovieEventId movieEventId = new();
        MovieEvent movieEvent = MovieEvent.Create(new(), new(), new(2099, 5, 10, 15, 0, 0), 100, movieEventId);
        await _repository.Save(movieEvent);
        BookMovieEventInput input = new(movieEventId.Value, 1, 0);

        //Act + Assert
        var result = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _bookMovieEvent.Execute(input));
        Assert.Equal($"Bookings for the event with id {movieEventId} haven't opened yet", result.Message);
    }
}
