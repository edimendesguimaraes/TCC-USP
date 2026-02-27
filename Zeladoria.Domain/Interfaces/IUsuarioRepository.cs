using Zeladoria.Domain.Entities;

namespace Zeladoria.Domain.Interfaces;

public interface IUsuarioRepository
{    
    Task<Usuario?> ObterPorExternalAuthIdAsync(string externalAuthId);    
    Task AdicionarAsync(Usuario usuario);
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Usuario>> ObterTodosAsync();
    Task AtualizarAsync(Usuario usuario);
}