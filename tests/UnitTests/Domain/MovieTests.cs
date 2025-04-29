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

        [Fact]
        public void Create_InvalidTitle_ShouldThrowError()
        {
            //Arrange
            MovieId movieId = new();

            //Act + Assert
            Assert.Throws<ArgumentException>(() =>Movie.Create("", "It's minecrafting time", 2025, 120, "Every Genre", "Jack Black, Jason Momoa", 3, "example.com/poster.png", movieId));
        }

        [Fact]
        public void Create_InvalidDescription_ShouldThrowError()
        {
            //Arrange
            MovieId movieId = new();
            //Act+Assert
            Assert.Throws<ArgumentException>(() => Movie.Create("A minecraft movie", "", 2025, 120, "Every Genre", "Jack Black, Jason Momoa", 3, "example.com/poster.png", movieId));
        }

        [Fact]
        public void Create_WithYearInFuture_ShouldThrowError()
        {
            //Arrange
            MovieId movieId = new();
            //Act+Assert
            Assert.Throws<ArgumentException>(() => Movie.Create("A minecraft movie", "It's minecrafting time", 2077, 120, "Every Genre", "Jack Black, Jason Momoa", 3, "example.com/poster.png", movieId));
        }

        [Fact]
        public void Create_WithNegativeDuration_ShouldThrowError()
        {
            //Arrange
            MovieId movieId = new();
            //Act+Assert
            Assert.Throws<ArgumentException>(() => Movie.Create("A minecraft movie", "It's minecrafting time", 2025, -1, "Every Genre", "Jack Black, Jason Momoa", 3, "example.com/poster.png", movieId));
        }

        [Fact]
        public void Create_WithInvalidGenre_ShouldThrowError()
        {
            //Arrange
            MovieId movieId = new();
            //Act+Assert
            Assert.Throws<ArgumentException>(() => Movie.Create("A minecraft movie", "It's minecrafting time", 2025, 120, "", "Jack Black, Jason Momoa", 3, "example.com/poster.png", movieId));
        }

        [Fact]
        public void Create_WithoutActors_ShouldThrowError()
        {
            //Arrange
            MovieId movieId = new();
            //Act+Assert
            Assert.Throws<ArgumentException>(() => Movie.Create("A minecraft movie", "It's minecrafting time", 2025, 120, "Every Genre", "", 3, "example.com/poster.png", movieId));
        }

        [Fact]
        public void Create_WithNegativeAgeRating_ShouldThrowError()
        {
            //Arrange
            MovieId movieId = new();
            //Act+Assert
            Assert.Throws<ArgumentException>(() => Movie.Create("A minecraft movie", "It's minecrafting time", 2025, 120, "Every Genre", "Jack Black, Jason Momoa", -1, "example.com/poster.png", movieId));
        }

        [Fact]
        public void Create_WithoutPoster_ShouldThrowError()
        {
            //Arrange
            MovieId movieId = new();
            //Act+Assert
            Assert.Throws<ArgumentException>(() => Movie.Create("A minecraft movie", "It's minecrafting time", 2025, 120, "Every Genre", "Jack Black, Jason Momoa", 3, "", movieId));
        }
    }
}
