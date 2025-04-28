using Howestprime.Movies.Domain.Movie;

namespace UnitTests.Domain
{
    public class MovieTests
    {
        [Fact]
        public void Create_WithValidData_ShouldCreateUser()
        {
            //Arrange + Act
            MovieId movieId = new();
            Movie movie = Movie.Create("A minecraft movie", "It's minecrafting time", 2025, 120, "Every Genre", "Jack Black, Jason Momoa", 3, "example.com/poster.png", movieId);

            //Assert
            Assert.NotNull(movie);
            Assert.Equal("A minecraft movie", movie.Title);
            Assert.Equal("It's minecrafting time", movie.Description);
            Assert.Equal(2025, movie.Year);
            Assert.Equal(120, movie.Duration);
            Assert.Equal("Every Genre", movie.Genres);
            Assert.Equal("Jack Black, Jason Momoa", movie.Actors);
            Assert.Equal(3, movie.AgeRating);
            Assert.Equal("example.com/poster.png", movie.PosterUrl);
            Assert.Equal(movieId, movie.Id);
        }
    }
}
