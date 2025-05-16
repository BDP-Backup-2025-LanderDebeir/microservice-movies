using System.Diagnostics.CodeAnalysis;

namespace Howestprime.Movies.Application.Contracts.Data;

[ExcludeFromCodeCoverage]
public sealed record MovieData
{
    public required string Id { get; set; }
    public required string PosterUrl { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required int AgeRating { get; set; }
    public required int Year { get; set; }
    public required int Duration { get; set; }
    public required string Actors { get; set; }
    public required string Description { get; set; }
    public required List<MovieEventData> Events { get; set; }
}
