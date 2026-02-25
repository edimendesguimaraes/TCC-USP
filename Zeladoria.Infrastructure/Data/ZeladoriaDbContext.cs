using Microsoft.EntityFrameworkCore;
using Zeladoria.Domain.Entities;

namespace Zeladoria.Infrastructure.Data;

public class ZeladoriaDbContext : DbContext
{
    public ZeladoriaDbContext(DbContextOptions<ZeladoriaDbContext> options) : base(options) { }

 
    public DbSet<Ocorrencia> Ocorrencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
 
        modelBuilder.Entity<Ocorrencia>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
        });
    }
}