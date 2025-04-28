using Aornis;
using Howestprime.Movies.Domain.Movie;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public sealed class EFMovieRepository(
    DomainContextBase context
    ) : IMovieRepository
{
    private readonly DomainContextBase _context = context;
    public Task<Optional<Movie>> ById(MovieId id)
    {
        return _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id)
            .ContinueWith(task => Optional.Of(task.Result));
    }

    public Task Remove(Movie entity)
    {
        _context.Movies.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task Save(Movie entity)
    {
        bool exists = await _context.Movies.AnyAsync(m => m.Id == entity.Id);

        if (exists)
        {
            _context.Movies.Update(entity);
            return;
        }
        _context.Movies.Add(entity);
    }
}
