namespace Zeladoria.Application.DTOs;

public class RegistrarUsuarioDto
{    
    public string ExternalAuthId { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}