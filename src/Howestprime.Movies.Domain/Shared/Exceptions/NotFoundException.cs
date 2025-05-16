using System.Diagnostics.CodeAnalysis;

namespace Howestprime.Movies.Domain.Shared.Exceptions;

 [ExcludeFromCodeCoverage]
public sealed class NotFoundException(string message) : Exception(message)
{
}