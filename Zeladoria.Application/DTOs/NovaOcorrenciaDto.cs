using Zeladoria.Domain.Enums;

namespace Zeladoria.Application.DTOs;

public class NovaOcorrenciaDto
{    
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public CategoriaProblema Categoria { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? FotoUrl { get; set; }
}