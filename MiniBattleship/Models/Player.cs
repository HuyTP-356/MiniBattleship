namespace MiniBattleship.Models
{
    public class Player
    {
        public string Name { get; set; }
        public GameBoard Board { get; set; } = new GameBoard();
        public bool IsBot { get; set; }
    }
}