using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TicTacToe.Services.Game.Contracts;
using TicTacToe.Services.Player.Contracts;

namespace TicTacToe.Presentation.Controllers;

public class GameController : Controller
{
    private readonly IGameService gameService;
    private readonly IPlayerService playerService;

    public GameController(IGameService gameService, IPlayerService playerService)
    {
        this.gameService = gameService;
        this.playerService = playerService;
    }

    public IActionResult Index()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> StartGame(string player1Name, string player1Color, string player2Name, string player2Color)
    {
        var player1 = await this.playerService.CreatePlayerAsync(player1Name, player1Color);
        var player2 = await this.playerService.CreatePlayerAsync(player2Name, player2Color);

        int gameId = await this.gameService.StartGameAsync(player1.PlayerId, player2.PlayerId);

        return this.RedirectToAction("GameBoard", new { gameId });
    }

    [HttpGet]
    public IActionResult GameBoard(int gameId)
    {
        
        return View();
    }

    [HttpGet]
    public IActionResult GameHistory()
    {
      
        return View();
    }
}