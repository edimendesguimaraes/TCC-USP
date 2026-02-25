using Zeladoria.Domain.Enums;

namespace Zeladoria.Domain.Entities;

public class Ocorrencia
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; } 
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public CategoriaProblema Categoria { get; private set; }
    public StatusOcorrencia Status { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string? FotoUrl { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAtualizacao { get; private set; }

    protected Ocorrencia() { }
    
    public Ocorrencia(Guid usuarioId, string titulo, string descricao, CategoriaProblema categoria, double latitude, double longitude, string? fotoUrl)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        Titulo = titulo;
        Descricao = descricao;
        Categoria = categoria;
        Latitude = latitude;
        Longitude = longitude;
        FotoUrl = fotoUrl;
        Status = StatusOcorrencia.Aberta;
        DataCriacao = DateTime.UtcNow;
    }

    public void AtualizarStatus(StatusOcorrencia novoStatus)
    {
        Status = novoStatus;
        DataAtualizacao = DateTime.UtcNow;
    }
}