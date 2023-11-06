using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClientesApi.Modelo;

public partial class DbclienteContext : DbContext
{
    public DbclienteContext()
    {
    }

    public DbclienteContext(DbContextOptions<DbclienteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__D5946642CD4D8700");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(120);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.CodigoPaisNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CodigoPais)
                .HasConstraintName("FK__Clientes__Codigo__38996AB5");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.CodigoPais).HasName("PK__Pais__BA1451F59F804397");

            entity.Property(e => e.NombrePais).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
