namespace TicTacToe.Services.Requests;

public class PlayerRequest
{
    public Guid PlayerId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}