using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.Movie;
using Howestprime.Movies.Domain.MovieEvent;
using UnitTests.Shared;

namespace UnitTests.Application;

public class ScheduleMovieEventTests
{
    private readonly MockMovieEventRepository _movieEventRepository;
    private readonly MockMovieRepository _movieRepository;
    private readonly MockUnitOfWork _unitOfWork;
    private readonly MockTestLogger<ScheduleMovieEvent> _logger;
    private readonly ScheduleMovieEvent _scheduleMovieEvent;

    public ScheduleMovieEventTests()
    {
        _movieEventRepository = new MockMovieEventRepository();
        _movieRepository = new MockMovieRepository();
        _unitOfWork = new MockUnitOfWork();
        _logger = new MockTestLogger<ScheduleMovieEvent>();
        _scheduleMovieEvent = new ScheduleMovieEvent(_movieEventRepository, _movieRepository, _unitOfWork, _logger);
    }

    [Fact]
    public async Task Execute_WithValidInput_ShouldScheduleEventAsync()
    {
        //Arrange
        Room room = new(new RoomId(), "Velet Room", 100);
        _movieEventRepository.Rooms.Add(room);
        Movie movie = Movie.Create("A minecraft movie", "It's minecrafting time", 2025, 120, "Every Genre", "Jack Black, Jason Momoa", 3, "example.com/poster.png");
        await _movieRepository.Save(movie);
        DateTime time = new(2099, 12, 31, 15, 0, 0);
        ScheduleMovieEventInput input = new(movie.Id.Value, time, room.Id.Value);

        //Act
        var result = await _scheduleMovieEvent.Execute(input);

        //Assert
        Assert.Single(_movieEventRepository.SavedMovieEvents);

        var movieEvent = _movieEventRepository.SavedMovieEvents[0];

        Assert.Equal(result, movieEvent.Id.Value);
        Assert.Equal(movie.Id, movieEvent.MovieId);
        Assert.Equal(time, movieEvent.Time);
        Assert.Equal(room.Id, movieEvent.RoomId);
        Assert.Equal(room.Capacity, movieEvent.Capacity);
    }

    [Fact]
    public async Task Execute_InvalidInput_ShouldThrowError()
    {
        //Arrange
        MovieId invalidId = new();
        ScheduleMovieEventInput input = new(invalidId.Value, DateTime.Now, new RoomId().Value);

        //Act + Assert
        _ = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _scheduleMovieEvent.Execute(input));
        Assert.Empty(_movieEventRepository.SavedMovieEvents);
    }
}