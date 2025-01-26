using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Services.Game.Contracts;

namespace TicTacToe.Services.Game
{
    public class GameService : IGameService
    {
        private readonly TicTacToeDbContext context;

        public GameService(TicTacToeDbContext context)
        {
            this.context = context;
        }

        public Data.Models.Game StartNewGame(Guid player1Id, Guid player2Id)
        {
            var game = new Data.Models.Game
            {
                Player1Id = player1Id,
                Player2Id = player2Id,
                CurrentTurnPlayerId = player1Id,
                DatePlayed = DateTime.Now,
                Board = string.Join(",", Enumerable.Repeat(" ", 9)),
                Result = "In Progress",
            };

            this.context.Games.Add(game);
            this.context.SaveChanges();
            return game;
        }

        public Data.Models.Game PlayTurn(int gameId, int spaceId)
        {
            var game = this.context.Games.FirstOrDefault(g => g.GameId == gameId);

            if (game == null)
            {
                throw new Exception("Game not found.");
            }

            var board = game.Board.Split(',').ToArray();
            if (board[spaceId - 1] == "x" || board[spaceId - 1] == "o")
            {
                throw new Exception("Space already taken.");
            }

            var symbol = game.CurrentTurnPlayerId == game.Player1Id ? "x" : "o";
            board[spaceId - 1] = symbol;

            game.Board = string.Join(",", board);
            game.CurrentTurnPlayerId = game.CurrentTurnPlayerId == game.Player1Id ? game.Player2Id : game.Player1Id;

            var winner = this.CheckWinner(board);
            if (winner != null)
            {
                game.Result = winner;
            }
            else if (board.All(cell => cell == "x" || cell == "o"))
            {
                game.Result = "Draw";
            }

            this.context.Games.Update(game);
            this.context.SaveChanges();

            return game;
        }

        public string CheckWinner(string[] board)
        {
            int[][] winningCombinations =
            {
                new[] { 0, 1, 2 },
                new[] { 3, 4, 5 },
                new[] { 6, 7, 8 },
                new[] { 0, 3, 6 },
                new[] { 1, 4, 7 },
                new[] { 2, 5, 8 },
                new[] { 0, 4, 8 },
                new[] { 2, 4, 6 },
            };

            foreach (var combo in winningCombinations)
            {
                if (!string.IsNullOrWhiteSpace(board[combo[0]]) && 
                    board[combo[0]] == board[combo[1]] && 
                    board[combo[1]] == board[combo[2]])
                {
                    return board[combo[0]] == "x" ? "Player 1 Wins" : "Player 2 Wins";
                }
            }

            return null;
        }

        public Data.Models.Game GetGameById(int gameId)
        {
            return this.context.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .FirstOrDefault(g => g.GameId == gameId);
        }

        public IEnumerable<Data.Models.Game> GetGameHistory()
        {
            return this.context.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .Where(g => g.Result != "In Progress")
                .ToList();
        }
    }
}
