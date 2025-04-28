using Domaincrafters.Domain;

namespace Howestprime.Movies.Domain.MovieEvent;

public sealed class MovieEventId(string? id = ""): UuidEntityId(id);
