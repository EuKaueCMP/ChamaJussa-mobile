using System;
using System.Collections.Generic;
using ChamaJussaAPI.Domains;
using Microsoft.EntityFrameworkCore;

namespace ChamaJussaAPI.Contexts;

public partial class ChamaJussaContext : DbContext
{
    public ChamaJussaContext()
    {
    }

    public ChamaJussaContext(DbContextOptions<ChamaJussaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fila> Fila { get; set; }

    public virtual DbSet<Localizacao> Localizacao { get; set; }

    public virtual DbSet<OrdemServico> OrdemServico { get; set; }

    public virtual DbSet<StatusItem> StatusItem { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=ChamaJussa; Trusted_Connection=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fila>(entity =>
        {
            entity.HasKey(e => e.filaID).HasName("PK__Fila__AA00B3D07C9F9E3B");

            entity.Property(e => e.nomeFila)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Localizacao>(entity =>
        {
            entity.HasKey(e => e.localizacaoID).HasName("PK__Localiza__9B95853B527770C8");

            entity.Property(e => e.andar).HasMaxLength(50);
            entity.Property(e => e.nome).HasMaxLength(100);
        });

        modelBuilder.Entity<OrdemServico>(entity =>
        {
            entity.HasKey(e => e.ordemServicoID).HasName("PK__OrdemSer__C68E19A8F0512CB4");

            entity.Property(e => e.dataCriacao).HasPrecision(0);
            entity.Property(e => e.descricao).HasMaxLength(255);
            entity.Property(e => e.imagem)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.nomeItem)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.fila).WithMany(p => p.OrdemServico)
                .HasForeignKey(d => d.filaID)
                .HasConstraintName("ordemServico_FilaID_FK");

            entity.HasOne(d => d.localizacao).WithMany(p => p.OrdemServico)
                .HasForeignKey(d => d.localizacaoID)
                .HasConstraintName("ordemServico_Localizacao_FK");

            entity.HasOne(d => d.status).WithMany(p => p.OrdemServico)
                .HasForeignKey(d => d.statusID)
                .HasConstraintName("ordemServico_Status_FK");

            entity.HasOne(d => d.usuarioSolicitanteNavigation).WithMany(p => p.OrdemServico)
                .HasForeignKey(d => d.usuarioSolicitante)
                .HasConstraintName("ordemServico_usuarioSolicitante_FK");
        });

        modelBuilder.Entity<StatusItem>(entity =>
        {
            entity.HasKey(e => e.statusID).HasName("PK__StatusIt__36257A381938B9F9");

            entity.Property(e => e.nomeStatus).HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.usuarioID).HasName("PK__Usuario__A5B1ABAED66DE6DD");

            entity.HasIndex(e => e.email, "UQ__Usuario__AB6E6164C3A8DC3F").IsUnique();

            entity.HasIndex(e => e.NIF, "UQ__Usuario__C7DEC3306F7AA582").IsUnique();

            entity.Property(e => e.usuarioID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NIF)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
