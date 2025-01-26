namespace TicTacToe.Data.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public Guid Player1Id { get; set; }
        public Guid Player2Id { get; set; }
        public string Board { get; set; } = "1,2,3,4,5,6,7,8,9";
        public string Result { get; set; } = "In Progress";
        public Guid CurrentTurnPlayerId { get; set; }
        public DateTime DatePlayed { get; set; }

        public Player Player1 { get; set; } = null!;
        public Player Player2 { get; set; } = null!;
    }
}