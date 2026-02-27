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
    private readonly IOcorrenciaRepository _ocorrenciaRepository;
    private readonly IUsuarioRepository _usuarioRepository; 

    public OcorrenciasController(IOcorrenciaRepository ocorrenciaRepository, IUsuarioRepository usuarioRepository)
    {
        _ocorrenciaRepository = ocorrenciaRepository;
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarOcorrencia([FromBody] NovaOcorrenciaDto dto)
    {
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(usuarioIdClaim) || !Guid.TryParse(usuarioIdClaim, out var usuarioId))
            return Unauthorized("Token inválido.");

        var novaOcorrencia = new Ocorrencia(usuarioId, dto.Titulo, dto.Descricao, dto.Categoria, dto.Latitude, dto.Longitude, dto.FotoUrl);
        await _ocorrenciaRepository.AdicionarAsync(novaOcorrencia);

        // GAMIFICAÇÃO: Dá 10 pontos pela criação
        var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);
        if (usuario != null)
        {
            usuario.AdicionarPontos(10);
            await _usuarioRepository.AtualizarAsync(usuario);            
        }

        return StatusCode(201, novaOcorrencia);
    }

    
    [HttpGet("todas")]
    public async Task<IActionResult> ListarTodasOcorrencias()
    {
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(usuarioIdClaim) || !Guid.TryParse(usuarioIdClaim, out var usuarioId))
            return Unauthorized("Token inválido.");

        var ocorrencias = await _ocorrenciaRepository.ObterTodasAsync();
        return Ok(ocorrencias);
    }

    // NOVA ROTA OTIMIZADA PARA O APLICATIVO
    [HttpGet("minhas")]
    public async Task<IActionResult> ListarMinhasOcorrencias()
    {
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(usuarioIdClaim) || !Guid.TryParse(usuarioIdClaim, out var usuarioId))
            return Unauthorized("Token inválido.");

        var ocorrencias = await _ocorrenciaRepository.ObterPorUsuarioAsync(usuarioId);
        return Ok(ocorrencias);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] AtualizarStatusDto dto)
    {
        var ocorrencia = await _ocorrenciaRepository.ObterPorIdAsync(id);
        if (ocorrencia == null) return NotFound("Ocorrência não encontrada.");

        // GAMIFICAÇÃO: Atualiza o status e pega quantos pontos extras a pessoa ganhou
        int pontosGanhos = ocorrencia.AtualizarStatus(dto.NovoStatus, dto.RespostaPrefeitura);
        await _ocorrenciaRepository.AtualizarAsync(ocorrencia);

        if (pontosGanhos > 0)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(ocorrencia.UsuarioId);
            if (usuario != null)
            {
                usuario.AdicionarPontos(pontosGanhos);                
                await _usuarioRepository.AtualizarAsync(usuario);
            }
        }

        return NoContent();
    }
}