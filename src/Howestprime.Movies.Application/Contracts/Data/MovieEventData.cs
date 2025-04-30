namespace Howestprime.Movies.Application.Contracts.Data;

public sealed record MovieEventData
{
    public required string Id { get; set; }
    public required DateTime Time { get; set; }
    public required string RoomId { get; set; }
    public required string MovirId { get; set; }
    public required int Capacity { get; set; }
    public required RoomData Room { get; set; }
    public required MovieData Movie { get; set; }
}