using Howestprime.Movies.Application.Movies;
using UnitTests.Shared;

namespace UnitTests.Application;

public class RegisterMovieTests
{
    private readonly MockMovieRepository _movieRepository;
    private readonly MockUnitOfWork _unitOfWork;
    private readonly MockTestLogger<RegisterMovie> _logger;
    private readonly RegisterMovie _registerMovie;

    public RegisterMovieTests()
    {
        _movieRepository = new MockMovieRepository();
        _unitOfWork = new MockUnitOfWork();
        _logger = new MockTestLogger<RegisterMovie>();
        _registerMovie = new RegisterMovie(_movieRepository, _unitOfWork, _logger);
    }

    [Fact]
    public async Task Execute_WithValidInput_ShouldRegisterMovie()
    {
        //Arrange
        RegisterMovieInput input = new RegisterMovieInput("A minecraft movie", "It's minecrafting time", 120, "Every Genre", 2025, "Jack Black, Jason Momoa", 3, "example.com/poster.png");

        //Act
        var result = await _registerMovie.Execute(input);

        //Assert
        Assert.Single(_movieRepository.SavedMovies);

        var savedMovie = _movieRepository.SavedMovies[0];

        Assert.Equal(result, savedMovie.Id.Value);
        Assert.Equal(input.Title, savedMovie.Title);
        Assert.Equal(input.Description, savedMovie.Description);
        Assert.Equal(input.Duration, savedMovie.Duration);
        Assert.Equal(input.Genre, savedMovie.Genres);
        Assert.Equal(input.Year, savedMovie.Year);
        Assert.Equal(input.Actors, savedMovie.Actors);
        Assert.Equal(input.AgeRating, savedMovie.AgeRating);
        Assert.Equal(input.PosterUrl, savedMovie.PosterUrl);
    }

    [Fact]
    public async Task Execute_InvalidData_ShouldThrowException()
    {
        //Arrange
        RegisterMovieInput input = new RegisterMovieInput("", "", -1, "", -1, "", -1, "");

        //Act + Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _registerMovie.Execute(input));
        Assert.Contains("Movie title can not be empty", exception.Message);
        Assert.Empty(_movieRepository.SavedMovies);
    }
}
