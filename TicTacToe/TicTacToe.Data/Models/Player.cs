﻿namespace TicTacToe.Data.Models;

public class Player
{
    public Guid PlayerId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}