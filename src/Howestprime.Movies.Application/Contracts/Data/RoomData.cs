using System.Diagnostics.CodeAnalysis;

namespace Howestprime.Movies.Application.Contracts.Data;

[ExcludeFromCodeCoverage]
public sealed record RoomData
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required int Capacity { get; set; }
}