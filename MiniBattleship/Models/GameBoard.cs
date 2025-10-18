namespace MiniBattleship.Models
{
    public class GameBoard
    {
        public const int Size = 7;
        public CellState[,] Grid { get; set; } = new CellState[Size, Size];
        public List<Ship> Ships { get; set; } = new List<Ship>();
    }
}