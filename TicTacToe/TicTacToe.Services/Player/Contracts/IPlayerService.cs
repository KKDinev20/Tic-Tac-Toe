using TicTacToe.Data.Models;

namespace TicTacToe.Services.Game.Contracts;

public interface IPlayerService
{
    Data.Models.Player CreatePlayer(string name, string color);
}