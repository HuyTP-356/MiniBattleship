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
        public IActionResult Shoot(int row, int col)
        {
            _gameService.HandlePlayerShot(row, col);
            // Trả về trạng thái game mới nhất dưới dạng JSON để cập nhật bằng JavaScript
            return Json(_gameService.GetGameState());
        }
    }
}