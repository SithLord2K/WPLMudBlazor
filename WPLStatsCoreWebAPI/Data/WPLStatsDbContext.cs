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
    public virtual DbSet<TeamDetails> TeamDetails { get; set; }
    public virtual DbSet<Week> Weeks { get; set; }
    public virtual DbSet<PlayerData> PlayerData { get; set; }
    public virtual DbSet<Schedule> Schedule { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("wiley");

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<TeamDetails>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Teams");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Captain_Player_Id).HasMaxLength(50);
            entity.Property(e => e.TeamName).HasMaxLength(50);
        });

        modelBuilder.Entity<Week>(entity =>
        {
            entity.HasKey(e => e.WeekNumber);

            entity.Property(e => e.WeekNumber).ValueGeneratedNever();
            entity.Property(e => e.DatePlayed).HasColumnType("date");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<PlayerData>(entity =>
        {
            entity.HasKey(e => e.PlayerId);
            entity.Property(e => e.GamesWon);
            entity.Property(e => e.GamesLost);
            entity.Property(e => e.WeekNumber);
            
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.Property(e => e.Week_Id).HasColumnType("integer");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    
}
