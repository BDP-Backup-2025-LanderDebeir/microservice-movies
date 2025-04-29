using Howestprime.Movies.Domain.MovieEvent;

namespace UnitTests.Domain;

public class RoomTests
{
    [Fact]
    public void Create_WithValidTitle_ShouldCreateRoom()
    {
        //Arrange + Act
        RoomId roomId = new();
        Room room = Room.Create("Velvet room", 500, roomId);

        //Assert
        Assert.NotNull(room);
        Assert.Equal(roomId, room.Id);
        Assert.Equal("Velvet room", room.Name);
        Assert.Equal(500, room.Capacity);
    }

    [Fact]
    public void Create_WithInvalidName_ShouldThrowError()
    {
        //Arrange
        RoomId roomId = new();

        //Act + Assert
        Assert.Throws<ArgumentException>(() => Room.Create("", 500, roomId));
    }

    [Fact]
    public void Create_WithInvalidNumver_ShouldThrowError()
    {
        //Arrange
        RoomId roomId = new();

        //Act + Assert
        Assert.Throws<ArgumentException>(() => Room.Create("Velvet Room", -1, roomId));
    }
}