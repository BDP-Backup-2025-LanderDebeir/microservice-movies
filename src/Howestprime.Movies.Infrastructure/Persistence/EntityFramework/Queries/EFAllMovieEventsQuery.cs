using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries;

public sealed class EFAllMovieEventsQuery(
    QueryContextBase context
    ) : IAllMovieEventsQuery
{
    private readonly QueryContextBase _context = context;

    public async Task<IReadOnlyList<MovieEventData>> Fetch(Expression<Func<MovieEventData, bool>> filter)
    {
        return await _context.MovieEvents
            .Where(filter)
            .Include(e => e.Movie)
            .Include(e => e.Room)
            .ToListAsync();
    }
}
