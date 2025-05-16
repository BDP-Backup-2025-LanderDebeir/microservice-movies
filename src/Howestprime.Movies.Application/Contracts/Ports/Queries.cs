using Howestprime.Movies.Application.Contracts.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Howestprime.Movies.Application.Contracts.Ports;

public interface IAllMoviesQuery
{
    Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> filter);
}

public interface IFindMovieByIdQuery
{
    Task<MovieData?> Fetch(string id);
}

public interface IFindMovieByIdWithEventsQuery
{
    Task<MovieData?> Fetch(string id);
}

public interface IAllMovieEventsQuery
{
    Task<IReadOnlyList<MovieEventData>> Fetch(Expression<Func<MovieEventData, bool>> filter);
}

public interface IAllMoviesWithEventsQuery
{
    Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> filter);
}