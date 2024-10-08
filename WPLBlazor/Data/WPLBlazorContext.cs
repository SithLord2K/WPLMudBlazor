using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WPLBlazor.Models;

namespace WPLBlazor.Data;

public partial class WPLBlazorContext : DbContext
{
    public WPLBlazorContext()
    {
    }

    public WPLBlazorContext(DbContextOptions<WPLBlazorContext> options)
        : base(options)
    {
    }

   public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerData> PlayerData { get; set; }

    public virtual DbSet<Schedules> Schedules { get; set; }

    public virtual DbSet<TeamDetails> TeamDetails { get; set; }

     public virtual DbSet<Weeks> Weeks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("wiley");

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Player");

            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
        });

        modelBuilder.Entity<PlayerData>(entity =>
        {
            entity.HasKey(e => e.PlayerId);

            entity.Property(e => e.PlayerId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Schedules>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tmp_Schedule");

            entity.ToTable("Schedule");

            entity.Property(e => e.Away_Team).HasColumnName("Away_Team");
            entity.Property(e => e.Home_Team).HasColumnName("Home_Team");
            entity.Property(e => e.Table_Number).HasColumnName("Table_Number");
            entity.Property(e => e.Week_Id).HasColumnName("Week_Id");
        });

        modelBuilder.Entity<TeamDetails>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Captain_Player_Id).HasColumnName("Captain_Player_Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TeamName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Weeks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Week");

            entity.Property(e => e.Away_Team).HasColumnName("Away_Team");
            entity.Property(e => e.Home_Team).HasColumnName("Home_Team");
            entity.Property(e => e.Home_Won).HasColumnName("Home_Won");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
