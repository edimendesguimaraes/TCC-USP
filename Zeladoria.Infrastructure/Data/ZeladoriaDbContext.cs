using Microsoft.EntityFrameworkCore;
using Zeladoria.Domain.Entities;

namespace Zeladoria.Infrastructure.Data;

public class ZeladoriaDbContext : DbContext
{
    public ZeladoriaDbContext(DbContextOptions<ZeladoriaDbContext> options) : base(options) { }

    public DbSet<Ocorrencia> Ocorrencias { get; set; }
    
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da Ocorrência
        modelBuilder.Entity<Ocorrencia>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);

            // Relacionamento: Uma Ocorrência pertence a um Usuário
            entity.HasOne<Usuario>()
                  .WithMany()
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração do Usuário
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ExternalAuthId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            
            entity.HasIndex(e => e.ExternalAuthId).IsUnique();
        });
    }
}