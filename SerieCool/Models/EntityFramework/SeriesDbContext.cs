using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SerieCool.Models.EntityFramework;

public partial class SeriesDbContext : DbContext
{
    public SeriesDbContext()
    {
    }

    public SeriesDbContext(DbContextOptions<SeriesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Serie> Series { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres;password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Serie>(entity =>
        {
            entity.HasKey(e => e.Serieid).HasName("serie_pkey");

            entity.ToTable("serie");

            entity.Property(e => e.Serieid).HasColumnName("serieid");
            entity.Property(e => e.Anneecreation).HasColumnName("anneecreation");
            entity.Property(e => e.Nbepisodes).HasColumnName("nbepisodes");
            entity.Property(e => e.Nbsaisons).HasColumnName("nbsaisons");
            entity.Property(e => e.Network)
                .HasMaxLength(50)
                .HasColumnName("network");
            entity.Property(e => e.Resume).HasColumnName("resume");
            entity.Property(e => e.Titre)
                .HasMaxLength(100)
                .HasColumnName("titre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
