using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services.Game.Contracts;

namespace TicTacToe.Presentation.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public IActionResult StartGame(Guid player1Id, Guid player2Id)
        {
            var game = this.gameService.StartNewGame(player1Id, player2Id);
            return this.RedirectToAction("Play", new { gameId = game.GameId });
        }

        [HttpGet]
        public IActionResult Play(int gameId)
        {
            var game = this.gameService.GetGameById(gameId);
            return this.View(game);
        }

        [HttpPost]
        public IActionResult PlayTurn(int gameId, int spaceId)
        {
            var game = this.gameService.PlayTurn(gameId, spaceId);
            if (game.Result != "In Progress")
            {
                return this.RedirectToAction("GameResult", new { gameId = game.GameId });
            }

            return this.RedirectToAction("Play", new { gameId = game.GameId });
        }

        [HttpGet]
        public IActionResult GameResult(int gameId)
        {
            var game = this.gameService.GetGameById(gameId);
            return this.View(game);
        }

        [HttpGet]
        public IActionResult History()
        {
            var games = this.gameService.GetGameHistory();
            return this.View(games);
        }
    }
}