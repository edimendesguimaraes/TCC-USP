using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Zeladoria.Application.DTOs;
using Zeladoria.Domain.Entities;
using Zeladoria.Domain.Interfaces;

namespace Zeladoria.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OcorrenciasController : ControllerBase
{
    private readonly IOcorrenciaRepository _repository;

    public OcorrenciasController(IOcorrenciaRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarOcorrencia([FromBody] NovaOcorrenciaDto dto)
    {
        // Pega o id do usuário logado a partir do token JWT
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(usuarioIdClaim) || !Guid.TryParse(usuarioIdClaim, out var usuarioId))
            return Unauthorized("Token inválido ou sem identificação do usuário.");

        var novaOcorrencia = new Ocorrencia(
            usuarioId, 
            dto.Titulo,
            dto.Descricao,
            dto.Categoria,
            dto.Latitude,
            dto.Longitude,
            dto.FotoUrl
        );

        await _repository.AdicionarAsync(novaOcorrencia);
        return CreatedAtAction(nameof(RegistrarOcorrencia), new { id = novaOcorrencia.Id }, novaOcorrencia);
    }

    [HttpGet]
    public async Task<IActionResult> ListarOcorrencias()
    {
        var ocorrencias = await _repository.ObterTodasAsync();
        return Ok(ocorrencias);
    }
    
    [HttpGet("usuario/{usuarioId}")]
    public async Task<IActionResult> ListarPorUsuario(Guid usuarioId)
    {
        var ocorrencias = await _repository.ObterPorUsuarioAsync(usuarioId);
        return Ok(ocorrencias);
    }
    
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] AtualizarStatusDto dto)
    {        
        var ocorrencia = await _repository.ObterPorIdAsync(id);

        if (ocorrencia == null)
            return NotFound("Ocorrência não encontrada.");
        
        ocorrencia.AtualizarStatus(dto.NovoStatus);        
        await _repository.AtualizarAsync(ocorrencia);
        return NoContent(); 
    }
}