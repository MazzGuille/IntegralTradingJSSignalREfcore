using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IntegralTradingJSSignalREfcore.Models;

public partial class DevExtremeContext : DbContext
{
    public DevExtremeContext()
    {
    }

    public DevExtremeContext(DbContextOptions<DevExtremeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hvicsv> Hvicsvs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hvicsv>(entity =>
        {
            entity.ToTable("HVIcsv");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ColorGrade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mic)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MIC");
            entity.Property(e => e.Sfi)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SFI");
            entity.Property(e => e.Strength).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrashId)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TrashID");
            entity.Property(e => e.Uhml)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("UHML");
            entity.Property(e => e.Ui)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("UI");

            entity.HasOne(d => d.Order).WithMany(p => p.Hvicsvs)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_HVIcsv_Orders");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
