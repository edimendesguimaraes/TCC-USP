using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zeladoria.Application.DTOs;
using Zeladoria.Domain.Entities;
using Zeladoria.Domain.Interfaces;

namespace Zeladoria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AuthController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration; // Para ler a chave secreta do appsettings
    }

    [HttpPost("login-simulado")]
    public async Task<IActionResult> LoginSimulado([FromBody] RegistrarUsuarioDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorExternalAuthIdAsync(dto.ExternalAuthId);

        if (usuario == null)
        {
            usuario = new Usuario(dto.ExternalAuthId, dto.Nome, dto.Email);
            await _usuarioRepository.AdicionarAsync(usuario);
        }

        // Gera o Token com o ID do usuário dentro dele
        var token = GerarTokenJwt(usuario);

        return Ok(new { Token = token, Usuario = usuario.Nome });
    }

    private string GerarTokenJwt(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2), // O token vale por 2 horas
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}