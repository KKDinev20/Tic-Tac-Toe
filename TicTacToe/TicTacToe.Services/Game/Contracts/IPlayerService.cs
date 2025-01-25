using TicTacToe.Services.Requests;

namespace TicTacToe.Services.Game.Contracts;

public interface IPlayerService
{
    public Task<PlayerRequest> CreatePlayerAsync(string name, string color);
    Task<PlayerRequest?> GetPlayerByIdAsync(Guid playerId);
}