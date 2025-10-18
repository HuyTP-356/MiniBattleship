namespace MiniBattleship.Models
{
    public class GameState
    {
        public Player[] Players { get; set; } = new Player[2];
        public int CurrentPlayerIndex { get; set; } = 0;
        public bool IsGameOver { get; set; } = false;
        public string WinnerMessage { get; set; } = string.Empty;
        public int TurnCount { get; set; } = 0;

        public GameState()
        {
            Players[0] = new Player { Name = "Người chơi 1", IsBot = false };
            Players[1] = new Player { Name = "BOT", IsBot = true };
        }
    }
}