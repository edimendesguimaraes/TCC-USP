using Microsoft.EntityFrameworkCore;
using Zeladoria.Domain.Entities;
using Zeladoria.Domain.Interfaces;
using Zeladoria.Infrastructure.Data;

namespace Zeladoria.Infrastructure.Repositories;

public class OcorrenciaRepository : IOcorrenciaRepository
{
    private readonly ZeladoriaDbContext _context;

    public OcorrenciaRepository(ZeladoriaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Ocorrencia ocorrencia)
    {
        await _context.Ocorrencias.AddAsync(ocorrencia);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Ocorrencia>> ObterTodasAsync()
    {        
        return await _context.Ocorrencias.AsNoTracking().ToListAsync();
    }

    public async Task<Ocorrencia?> ObterPorIdAsync(Guid id)
    {
        return await _context.Ocorrencias.FindAsync(id);
    }

    public async Task AtualizarAsync(Ocorrencia ocorrencia)
    {
        _context.Ocorrencias.Update(ocorrencia);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Guid id)
    {
        var ocorrencia = await ObterPorIdAsync(id);
        if (ocorrencia != null)
        {
            _context.Ocorrencias.Remove(ocorrencia);
            await _context.SaveChangesAsync();
        }
    }
}