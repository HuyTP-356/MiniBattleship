namespace MiniBattleship.Models
{
    public class GameBoard
    {
        public const int Size = 7;
        // THAY ĐỔI TỪ CellState[,] thành CellState[][]
        public CellState[][] Grid { get; set; }

        public List<Ship> Ships { get; set; } = new List<Ship>();

        public GameBoard()
        {
            // Khởi tạo mảng lồng nhau
            Grid = new CellState[Size][];
            for (int i = 0; i < Size; i++)
            {
                Grid[i] = new CellState[Size];
                // Mặc định tất cả các ô là Empty, mặc dù đây là giá trị mặc định của enum
                for (int j = 0; j < Size; j++)
                {
                    Grid[i][j] = CellState.Empty;
                }
            }
        }
    }
}