using Microsoft.EntityFrameworkCore;
using WPLBlazor.App.Models;

namespace WPLBlazor.App.Data
{
    public partial class WPLDBContext : DbContext
    {
        public WPLDBContext()
        {
        }

        public WPLDBContext(DbContextOptions<WPLDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<TeamDetails> TeamDetails { get; set; }
        public virtual DbSet<Weeks> Weeks { get; set; }
        public virtual DbSet<PlayerData> PlayerData { get; set; }
        public virtual DbSet<Schedules> Schedule { get; set; }

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

            modelBuilder.Entity<Weeks>(entity =>
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

            modelBuilder.Entity<Schedules>(entity =>
            {
                entity.Property(e => e.Week_Id).HasColumnType("integer");
                entity.Property(e => e.Date).HasColumnType("date");
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
