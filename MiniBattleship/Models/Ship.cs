//#### `Models/Ship.cs`
//Đại diện cho một chiếc tàu, bao gồm tọa độ và trạng thái đã bị chìm hay chưa.
//```csharp
namespace MiniBattleship.Models
{
    public class Ship
    {
        public List<(int Row, int Col)> Coordinates { get; set; } = new List<(int, int)>();
        public bool IsSunk { get; set; } = false;
        public int Size => Coordinates.Count;
    }
}