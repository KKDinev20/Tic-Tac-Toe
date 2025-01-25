namespace TicTacToe.Services.Requests;

public class GameHistoryRequest
{
    public int GameId { get; set; }
    public string Player1 { get; set; }
    public string Player2 { get; set; }
    public string Result { get; set; }
    public DateTime DatePlayed { get; set; }
}