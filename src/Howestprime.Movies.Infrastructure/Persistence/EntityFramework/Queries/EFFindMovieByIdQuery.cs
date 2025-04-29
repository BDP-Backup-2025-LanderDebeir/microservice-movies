using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries;

public sealed class EFFindMovieByIdQuery(
    QueryContextBase context
    ) : IFindMovieByIdQuery
{
    private QueryContextBase _context = context;

    public async Task<MovieData?> Fetch(string id)
    {
        return await _context.Movies
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
    }
}