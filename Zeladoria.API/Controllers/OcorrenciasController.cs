using Microsoft.AspNetCore.Mvc;
using Zeladoria.Application.DTOs;
using Zeladoria.Domain.Entities;
using Zeladoria.Domain.Interfaces;

namespace Zeladoria.API.Controllers;

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
        var novaOcorrencia = new Ocorrencia(
            dto.UsuarioId, 
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
}