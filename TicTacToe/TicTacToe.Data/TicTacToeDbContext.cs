using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.Models;

namespace TicTacToe.Data;

public class TicTacToeDbContext : DbContext
{
    public TicTacToeDbContext(DbContextOptions<TicTacToeDbContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.Entity<Player>().HasKey(s => s.PlayerId);
        modelBuilder.Entity<Game>().HasKey(s => s.GameId);
        
        modelBuilder.Entity<Game>()
            .HasOne(g => g.Player1)
            .WithMany()
            .HasForeignKey(g => g.Player1Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Game>()
            .HasOne(g => g.Player2)
            .WithMany()
            .HasForeignKey(g => g.Player2Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}