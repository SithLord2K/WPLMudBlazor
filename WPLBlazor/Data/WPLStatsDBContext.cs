using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WPLBlazor.Data.Models;

namespace WPLBlazor.Data;

public partial class WPLStatsDBContext : DbContext
{
    public WPLStatsDBContext() { }
    
    public WPLStatsDBContext(DbContextOptions<WPLStatsDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerData_Archive> PlayerData_Archives { get; set; }

    public virtual DbSet<PlayerDatum> PlayerData { get; set; }

    public virtual DbSet<PlayersView> PlayersViews { get; set; }

    public virtual DbSet<Players_Archive> Players_Archives { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Schedule_Archive> Schedule_Archives { get; set; }

    public virtual DbSet<TeamDetail> TeamDetails { get; set; }

    public virtual DbSet<TeamDetails_Archive> TeamDetails_Archives { get; set; }

    public virtual DbSet<Week> Weeks { get; set; }

    public virtual DbSet<WeeksView> WeeksViews { get; set; }

    public virtual DbSet<Weeks_Archive> Weeks_Archives { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("wiley");

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<PlayerData_Archive>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<PlayerDatum>(entity =>
        {
            entity.Property(e => e.PlayerId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<PlayersView>(entity =>
        {
            entity.ToView("PlayersView");
        });

        modelBuilder.Entity<Players_Archive>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tmp_Schedule");
        });

        modelBuilder.Entity<Schedule_Archive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tmp_Schedule_Archive");
        });

        modelBuilder.Entity<TeamDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TeamDetails_Archive>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Week>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Week");
        });

        modelBuilder.Entity<WeeksView>(entity =>
        {
            entity.ToView("WeeksView");
        });

        modelBuilder.Entity<Weeks_Archive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Week_Archive");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
