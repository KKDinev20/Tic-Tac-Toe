namespace TicTacToe.Data.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public int Player1Id { get; set; }
        public Player Player1 { get; set; }
        public int Player2Id { get; set; }
        public Player Player2 { get; set; }
        public string Result { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}
