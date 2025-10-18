using MiniBattleship.Models;

namespace MiniBattleship.Services
{
    public class GameService
    {
        private GameState _gameState;
        private readonly Random _random = new Random();
        private static readonly int[] ShipSizes = { 3, 2, 2 };

        public GameService()
        {
            NewGame();
        }

        public GameState GetGameState() => _gameState;

        public void NewGame()
        {
            _gameState = new GameState();
            PlaceShipsRandomly(_gameState.Players[0].Board);
            PlaceShipsRandomly(_gameState.Players[1].Board);
        }

        private void PlaceShipsRandomly(GameBoard board)
        {
            foreach (var size in ShipSizes)
            {
                bool placed = false;
                while (!placed)
                {
                    bool isHorizontal = _random.Next(2) == 0;
                    int row = _random.Next(GameBoard.Size);
                    int col = _random.Next(GameBoard.Size);

                    if (CanPlaceShip(board, row, col, size, isHorizontal))
                    {
                        var ship = new Ship();
                        for (int i = 0; i < size; i++)
                        {
                            int r = isHorizontal ? row : row + i;
                            int c = isHorizontal ? col + i : col;
                            board.Grid[r][c] = CellState.Ship; // SỬA Ở ĐÂY
                            ship.Coordinates.Add((r, c));
                        }
                        board.Ships.Add(ship);
                        placed = true;
                    }
                }
            }
        }

        private bool CanPlaceShip(GameBoard board, int row, int col, int size, bool isHorizontal)
        {
            for (int i = 0; i < size; i++)
            {
                int r = isHorizontal ? row : row + i;
                int c = isHorizontal ? col + i : col;

                if (r >= GameBoard.Size || c >= GameBoard.Size || board.Grid[r][c] != CellState.Empty) // SỬA Ở ĐÂY
                {
                    return false;
                }
            }
            return true;
        }

        public void HandlePlayerShot(int row, int col)
        {
            if (_gameState.IsGameOver) return;

            var opponent = _gameState.Players[1];
            var cell = opponent.Board.Grid[row][col]; // SỬA Ở ĐÂY

            if (cell == CellState.Hit || cell == CellState.Miss) return;

            if (cell == CellState.Ship)
            {
                opponent.Board.Grid[row][col] = CellState.Hit; // SỬA Ở ĐÂY
                CheckForSunkShips(opponent);
            }
            else
            {
                opponent.Board.Grid[row][col] = CellState.Miss; // SỬA Ở ĐÂY
            }

            _gameState.TurnCount++;
            CheckForWinner();
            if (!_gameState.IsGameOver)
            {
                _gameState.CurrentPlayerIndex = 1;
                BotMakeMove();
            }
        }

        private void BotMakeMove()
        {
            if (_gameState.IsGameOver) return;

            var opponent = _gameState.Players[0];
            var availableCells = new List<(int, int)>();

            for (int r = 0; r < GameBoard.Size; r++)
            {
                for (int c = 0; c < GameBoard.Size; c++)
                {
                    if (opponent.Board.Grid[r][c] == CellState.Empty || opponent.Board.Grid[r][c] == CellState.Ship) // SỬA Ở ĐÂY
                    {
                        availableCells.Add((r, c));
                    }
                }
            }

            if (availableCells.Count > 0)
            {
                var (row, col) = availableCells[_random.Next(availableCells.Count)];
                var cell = opponent.Board.Grid[row][col]; // SỬA Ở ĐÂY

                if (cell == CellState.Ship)
                {
                    opponent.Board.Grid[row][col] = CellState.Hit; // SỬA Ở ĐÂY
                    CheckForSunkShips(opponent);
                }
                else
                {
                    opponent.Board.Grid[row][col] = CellState.Miss; // SỬA Ở ĐÂY
                }
            }

            CheckForWinner();
            if (!_gameState.IsGameOver)
            {
                _gameState.CurrentPlayerIndex = 0;
            }
        }

        private void CheckForSunkShips(Player player)
        {
            foreach (var ship in player.Board.Ships)
            {
                if (!ship.IsSunk)
                {
                    bool allHit = ship.Coordinates.All(coord => player.Board.Grid[coord.Row][coord.Col] == CellState.Hit); // SỬA Ở ĐÂY
                    if (allHit)
                    {
                        ship.IsSunk = true;
                    }
                }
            }
        }

        private void CheckForWinner()
        {
            var player1 = _gameState.Players[0];
            var player2 = _gameState.Players[1];

            if (player2.Board.Ships.All(s => s.IsSunk))
            {
                _gameState.IsGameOver = true;
                _gameState.WinnerMessage = $"Người chơi 1 thắng sau {_gameState.TurnCount} lượt!";
            }
            else if (player1.Board.Ships.All(s => s.IsSunk))
            {
                _gameState.IsGameOver = true;
                _gameState.WinnerMessage = "BOT đã thắng!";
            }
        }
    }
}