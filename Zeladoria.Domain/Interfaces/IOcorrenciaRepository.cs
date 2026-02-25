using Zeladoria.Domain.Entities;

namespace Zeladoria.Domain.Interfaces;

public interface IOcorrenciaRepository
{
    Task AdicionarAsync(Ocorrencia ocorrencia);
    Task<IEnumerable<Ocorrencia>> ObterTodasAsync();
    Task<Ocorrencia?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Ocorrencia>> ObterPorUsuarioAsync(Guid usuarioId); 
    Task AtualizarAsync(Ocorrencia ocorrencia);
    Task ExcluirAsync(Guid id);
}