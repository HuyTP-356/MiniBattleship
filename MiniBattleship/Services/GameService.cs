using MiniBattleship.Models;

namespace MiniBattleship.Services
{
    public class GameService
    {
        private GameState _gameState;
        private readonly Random _random = new Random();
        private static readonly int[] ShipSizes = { 3, 2, 2 }; // Kích thước 3 tàu

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
                            board.Grid[r, c] = CellState.Ship;
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

                if (r >= GameBoard.Size || c >= GameBoard.Size || board.Grid[r, c] != CellState.Empty)
                {
                    return false; // Ra ngoài bàn cờ hoặc chồng lấn
                }
            }
            return true;
        }

        public void HandlePlayerShot(int row, int col)
        {
            if (_gameState.IsGameOver) return;

            var opponent = _gameState.Players[1]; // Người chơi luôn bắn vào BOT
            var cell = opponent.Board.Grid[row, col];

            if (cell == CellState.Hit || cell == CellState.Miss) return; // Ô này đã được bắn

            if (cell == CellState.Ship)
            {
                opponent.Board.Grid[row, col] = CellState.Hit;
                CheckForSunkShips(opponent);
            }
            else
            {
                opponent.Board.Grid[row, col] = CellState.Miss;
            }

            _gameState.TurnCount++;
            CheckForWinner();
            if (!_gameState.IsGameOver)
            {
                _gameState.CurrentPlayerIndex = 1; // Lượt của BOT
                BotMakeMove();
            }
        }

        private void BotMakeMove()
        {
            if (_gameState.IsGameOver) return;

            var opponent = _gameState.Players[0]; // BOT bắn vào người chơi
            var availableCells = new List<(int, int)>();

            for (int r = 0; r < GameBoard.Size; r++)
            {
                for (int c = 0; c < GameBoard.Size; c++)
                {
                    if (opponent.Board.Grid[r, c] == CellState.Empty || opponent.Board.Grid[r, c] == CellState.Ship)
                    {
                        availableCells.Add((r, c));
                    }
                }
            }

            if (availableCells.Count > 0)
            {
                var (row, col) = availableCells[_random.Next(availableCells.Count)];
                var cell = opponent.Board.Grid[row, col];

                if (cell == CellState.Ship)
                {
                    opponent.Board.Grid[row, col] = CellState.Hit;
                    CheckForSunkShips(opponent);
                }
                else
                {
                    opponent.Board.Grid[row, col] = CellState.Miss;
                }
            }

            CheckForWinner();
            if (!_gameState.IsGameOver)
            {
                _gameState.CurrentPlayerIndex = 0; // Trả lượt lại cho người chơi
            }
        }

        private void CheckForSunkShips(Player player)
        {
            foreach (var ship in player.Board.Ships)
            {
                if (!ship.IsSunk)
                {
                    bool allHit = ship.Coordinates.All(coord => player.Board.Grid[coord.Row, coord.Col] == CellState.Hit);
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