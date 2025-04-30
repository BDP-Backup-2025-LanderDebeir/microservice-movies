namespace Howestprime.Movies.Application.Contracts.Data;

public sealed record RoomData
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required int Capacity { get; set; }
}