using Domaincrafters.Domain;

namespace Howestprime.Movies.Domain.MovieEvent;

public sealed class RoomId(string? id = ""): UuidEntityId(id);

public class Room : Entity<RoomId>
{
    public string Name { get; private set; }
    public int Capacity { get; private set; }

    public Room(RoomId id, string name, int capacity) : base(id)
    {
        Name = name;
        Capacity = capacity;
    }

    public static Room Create(string name, int capacity, RoomId? id = null)
    {
        id ??= new RoomId();
        Room room = new Room(id, name, capacity);

        room.ValidateState();

        return room;
    }

    public override void ValidateState()
    {
       
    }

    private static void EnsureValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Room name can not be empty");
        }
    }

    private static void EnsureValidCapacity(int capacity)
    {
        if (int.IsNegative(capacity))
        {
            throw new ArgumentException("Room capacity can not be empty");
        }
    }
}