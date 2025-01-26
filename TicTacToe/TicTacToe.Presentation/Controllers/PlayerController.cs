using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services.Game.Contracts;

namespace TicTacToe.Presentation.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreatePlayers(string player1Name, string player1Color, string player2Name, string player2Color)
        {
            var player1 = this.playerService.CreatePlayer(player1Name, player1Color);
            var player2 = this.playerService.CreatePlayer(player2Name, player2Color);

            return this.RedirectToAction("StartGame", "Game", new { player1Id = player1.PlayerId, player2Id = player2.PlayerId });
        }
    }
}
