using Howestprime.Movies.Application.Contracts.Data;
using System.Linq.Expressions;

namespace Howestprime.Movies.Application.Contracts.Ports;

public interface IFindMovieQuery
{
    Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> filter);
}

public interface IFindMovieByIdQuery
{
    Task<MovieData?> Fetch(string id);
}