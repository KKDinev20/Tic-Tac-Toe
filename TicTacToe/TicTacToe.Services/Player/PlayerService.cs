using TicTacToe.Data;
using TicTacToe.Services.Game.Contracts;

namespace TicTacToe.Services.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly TicTacToeDbContext context;

        public PlayerService(TicTacToeDbContext context)
        {
            this.context = context;
        }

        public Data.Models.Player CreatePlayer(string name, string color)
        {
            var player = new Data.Models.Player { Name = name, Color = color };
            this.context.Players.Add(player);
            this.context.SaveChanges();
            return player;
        }
    }
}