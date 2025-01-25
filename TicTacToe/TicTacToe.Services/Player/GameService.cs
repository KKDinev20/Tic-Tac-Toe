using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TicTacToe.Data;
using TicTacToe.Data.Enums;
using TicTacToe.Services.Player.Contracts;
using TicTacToe.Services.Requests;

namespace TicTacToe.Services.Player;

public class GameService : IGameService
{
    private readonly TicTacToeDbContext ticTacToeDbContext;

    public GameService(TicTacToeDbContext ticTacToeDbContext)
    {
        this.ticTacToeDbContext = ticTacToeDbContext;
    }

    public async Task<int> StartGameAsync(Guid player1Id, Guid player2Id)
    {
        var player1 = await this.ticTacToeDbContext.Players.FindAsync(player1Id);
        var player2 = await this.ticTacToeDbContext.Players.FindAsync(player2Id);

        if (player1 == null || player2 == null)
            throw new ArgumentException("One or both players not found.");

        var game = new Data.Models.Game
        {
            Player1Id = player1Id,
            Player2Id = player2Id,
            DatePlayed = DateTime.UtcNow,
            Result = "In Progress",
            Board = JsonConvert.SerializeObject(new int[3, 3])
        };

        this.ticTacToeDbContext.Games.Add(game);
        await this.ticTacToeDbContext.SaveChangesAsync();

        return game.GameId;
    }

    public async Task<MoveResult> MakeMoveAsync(int gameId, Guid playerId, int x, int y)
    {
        var game = await this.ticTacToeDbContext.Games
            .Include(g => g.Player1)
            .Include(g => g.Player2)
            .FirstOrDefaultAsync(g => g.GameId == gameId);

        if (game == null)
            return MoveResult.GameNotFound;

        if (game.Result != "In Progress")
            return MoveResult.GameEnded;

        var board = JsonConvert.DeserializeObject<int[,]>(game.Board);

        var currentPlayer = game.Player1Id == playerId ? game.Player1Id : game.Player2Id;
        if (currentPlayer != playerId)
            return MoveResult.NotPlayerTurn;

        if (board[x, y] != 0)
            return MoveResult.InvalidMove;

        board[x, y] = playerId == game.Player1Id ? 1 : 2;

        if (CheckWin(board))
        {
            game.Result = $"{(playerId == game.Player1Id ? game.Player1.Name : game.Player2.Name)} Wins!";
        }
        else if (CheckDraw(board))
        {
            game.Result = "Draw";
        }

        game.Board = JsonConvert.SerializeObject(board);
        await ticTacToeDbContext.SaveChangesAsync();

        return MoveResult.Success;
    }

    public async Task<IEnumerable<GameHistoryRequest>> GetGameHistoryAsync()
    {
        var gameHistory = await ticTacToeDbContext.Games
            .Include(g => g.Player1)
            .Include(g => g.Player2)
            .OrderByDescending(g => g.DatePlayed)
            .ToListAsync();

        var gameHistoryRequests = gameHistory.Select(game => new GameHistoryRequest
        {
            GameId = game.GameId,
            Player1 = game.Player1.Name,
            Player2 = game.Player2.Name,
            Result = game.Result,
            DatePlayed = game.DatePlayed
        });

        return gameHistoryRequests;
    }


    public async Task<string> GetGameStatusAsync(int gameId)
    {
        var game = await ticTacToeDbContext.Games
            .FirstOrDefaultAsync(g => g.GameId == gameId);

        if (game == null)
            return "Game not found.";

        return game.Result;
    }

    private bool CheckWin(int[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return true;

            if (board[0, i] != 0 && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return true;
        }

        if (board[0, 0] != 0 && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            return true;

        if (board[0, 2] != 0 && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            return true;

        return false;
    }

    private bool CheckDraw(int[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 0)
                    return false;
            }
        }

        return true;
    }
}