using Microsoft.AspNetCore.Mvc;
using MiniBattleship.Services;

namespace MiniBattleship.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var gameState = _gameService.GetGameState();
            return View(gameState);
        }

        [HttpPost]
        public IActionResult NewGame()
        {
            _gameService.NewGame();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // <-- THÊM DÒNG NÀY
        public IActionResult Shoot(int row, int col)
        {
            // Logic xử lý lượt bắn của người chơi và sau đó là của bot
            _gameService.HandlePlayerShot(row, col);

            // Trả về trạng thái game MỚI NHẤT sau khi cả người và bot đã chơi
            return Json(_gameService.GetGameState());
        }
    }
}