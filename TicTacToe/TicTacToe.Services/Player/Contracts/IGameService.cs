using TicTacToe.Data.Enums;
using TicTacToe.Services.Requests;

namespace TicTacToe.Services.Player.Contracts;

public interface IGameService
{
    Task<int> StartGameAsync(Guid player1Id, Guid player2Id);
    Task<MoveResult> MakeMoveAsync(int gameId,  Guid playerId, int x, int y);
    Task<IEnumerable<GameHistoryRequest>> GetGameHistoryAsync();
    Task<string> GetGameStatusAsync(int gameId);
}