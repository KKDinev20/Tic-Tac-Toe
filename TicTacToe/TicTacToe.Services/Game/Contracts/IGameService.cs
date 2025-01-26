namespace TicTacToe.Services.Game.Contracts;

public interface IGameService
{
    Data.Models.Game StartNewGame(Guid player1Id, Guid player2Id);
    Data.Models.Game PlayTurn(int gameId, int spaceId);
    string CheckWinner(string[] board);
    Data.Models.Game GetGameById(int gameId);
    IEnumerable<Data.Models.Game> GetGameHistory();
}