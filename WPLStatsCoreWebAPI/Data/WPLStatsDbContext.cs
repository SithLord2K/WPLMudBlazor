using Microsoft.EntityFrameworkCore;
using WPLBlazor.API.Models;

namespace WPLBlazor.API.Data;

public partial class WPLStatsDbContext : DbContext
{
    public WPLStatsDbContext()
    {
    }

    public WPLStatsDbContext(DbContextOptions<WPLStatsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<PlayersView> PlayersView { get; set; }
    public virtual DbSet<TeamDetails> TeamDetails { get; set; }
    public virtual DbSet<Week> Weeks { get; set; }
    public virtual DbSet<PlayerData> PlayerData { get; set; }
    public virtual DbSet<Schedule> Schedule { get; set; }
    public virtual DbSet<WeeksView> WeeksView { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("wiley");

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName);
            entity.Property(e => e.LastName);
            entity.Property(e => e.TeamId);
        });
        modelBuilder.Entity<PlayersView>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.PlayerId);
            entity.Property(e => e.FirstName);
            entity.Property(e => e.LastName);
            entity.Property(e => e.GamesWon);
            entity.Property(e => e.GamesLost);
            entity.Property(e => e.GamesPlayed);
            entity.Property(e => e.WeekNumber); ;
        });

        modelBuilder.Entity<TeamDetails>(entity =>
        {
            entity.Property(e => e.Id);
            entity.Property(e => e.Captain_Player_Id);
            entity.Property(e => e.TeamName);
        });

        modelBuilder.Entity<Week>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.WeekNumber);
            entity.Property(e => e.Away_Team);
            entity.Property(e => e.Home_Team);
            entity.Property(e => e.Home_Won);
            entity.Property(e => e.Forfeit);
            entity.Property(e => e.Playoff);

        });

        modelBuilder.Entity<PlayerData>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.PlayerId);
            entity.Property(e => e.GamesWon);
            entity.Property(e => e.GamesLost);
            entity.Property(e => e.GamesPlayed);
            entity.Property(e => e.WeekNumber);
            
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Week_Id);
            entity.Property(e => e.Date);
            entity.Property(e => e.Home_Team);
            entity.Property(e => e.Away_Team);
            entity.Property(e => e.Table_Number);
        });
        modelBuilder.Entity<WeeksView>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.Week_Id);
            entity.Property(e => e.Home_Team);
            entity.Property(e => e.Away_Team);
            entity.Property(e => e.Forfeit);
            entity.Property(e => e.Playoff);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    
}
