using Zeladoria.Domain.Enums;

namespace Zeladoria.Application.DTOs;

public class AtualizarStatusDto
{
    public StatusOcorrencia NovoStatus { get; set; }
    public string? RespostaPrefeitura { get; set; } 
}