using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries;

public sealed class EFFindMovieByIdWithEventsQuery(
    QueryContextBase context
    ) : IFindMovieByIdWithEventsQuery
{
    private readonly QueryContextBase _context = context;

    public async Task<MovieData?> Fetch(string id)
    {
        return await _context.Movies
            .Include(m => m.Events.Select(e => e.Time.DayOfYear <= 14))
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}
