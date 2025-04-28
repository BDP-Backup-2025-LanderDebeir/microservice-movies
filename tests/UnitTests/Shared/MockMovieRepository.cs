
using Aornis;
using Howestprime.Movies.Domain.Movie;

namespace UnitTests.Shared;

public class MockMovieRepository : IMovieRepository
{
    public List<Movie> SavedMovies { get; } = new List<Movie>();
    public List<Movie> Movies { get; } = new List<Movie>();
    public Exception? ThrowOnSave { get; set; }
    public Task<Optional<Movie>> ById(MovieId id)
    {
        var movie = Movies.FirstOrDefault(m => m.Id.Value == id.Value);
        return Task.FromResult(Optional.Of<Movie>(movie));
    }
    
    public Task Remove(Movie entity)
    {
        throw new NotImplementedException();
    }

    public Task Save(Movie movie)
    {
        if (ThrowOnSave != null)
            throw ThrowOnSave;

        SavedMovies.Add(movie);

        var existingIndex = Movies.FindIndex(m => m.Id.Equals(movie.Id));
        if (existingIndex >= 0)
            Movies[existingIndex] = movie;
        else
            Movies.Add(movie);

        return Task.CompletedTask;
    }
}
