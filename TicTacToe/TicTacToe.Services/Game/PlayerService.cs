using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Services.Game.Contracts;
using TicTacToe.Services.Requests;

namespace TicTacToe.Services.Game;

public class PlayerService : IPlayerService
{
    private readonly TicTacToeDbContext ticTacToeDbContext;

    public PlayerService(TicTacToeDbContext ticTacToeDbContext)
    {
        this.ticTacToeDbContext = ticTacToeDbContext;
    }

    public async Task<PlayerRequest> CreatePlayerAsync(string name, string color)
    {
        var player = new Data.Models.Player { Name = name, Color = color };

        this.ticTacToeDbContext.Players.Add(player);
        await this.ticTacToeDbContext.SaveChangesAsync();

        return new PlayerRequest { PlayerId = player.PlayerId, Name = player.Name, Color = player.Color };
    }

    public async Task<PlayerRequest?> GetPlayerByIdAsync(Guid playerId)
    {
        var player = await this.ticTacToeDbContext.Players.FirstOrDefaultAsync(p => p.PlayerId == playerId);
        if (player == null)
        {
            return null;
        }

        return new PlayerRequest { PlayerId = player.PlayerId, Name = player.Name, Color = player.Color };
    }
}