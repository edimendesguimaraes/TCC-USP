using Microsoft.EntityFrameworkCore;
using Zeladoria.Domain.Entities;
using Zeladoria.Domain.Interfaces;
using Zeladoria.Infrastructure.Data;

namespace Zeladoria.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ZeladoriaDbContext _context;

    public UsuarioRepository(ZeladoriaDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterPorExternalAuthIdAsync(string externalAuthId)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.ExternalAuthId == externalAuthId);
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        return await _context.Usuarios.FindAsync(id);
    }
    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return await _context.Usuarios.AsNoTracking().ToListAsync();
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }
}