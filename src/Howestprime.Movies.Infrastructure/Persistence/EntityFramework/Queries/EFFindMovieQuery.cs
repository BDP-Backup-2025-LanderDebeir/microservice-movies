using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries;

public sealed class EFFindMovieQuery(
    QueryContextBase context
    ) : IFindMovieQuery
{
    private readonly QueryContextBase _context = context;

    public async Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> filter)
    {
        return await _context.Movies
            .Where(filter)
            .ToListAsync();
    }
}
