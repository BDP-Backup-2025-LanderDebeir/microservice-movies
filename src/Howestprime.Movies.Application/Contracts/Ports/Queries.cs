using Howestprime.Movies.Application.Contracts.Data;
using System.Linq.Expressions;

namespace Howestprime.Movies.Application.Contracts.Ports;

public interface IAllMoviesQuery
{
    Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> filter);
}