using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechChallenge.Application.DTO;
using TechChallenge.Application.Services;
using TechChallenge.Infra.Data;

namespace TechChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  

    private readonly TechChallengeService _techChallengeService;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;


    public WeatherForecastController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _techChallengeService = new TechChallengeService(_context, configuration);
        _configuration = configuration;
    }

    [HttpPost("CriarUsuario")]
    public async Task<IActionResult> CadastrarUsuario(UsuarioDTO usuario)
    {
        try
        {
            var ret = _techChallengeService.CadastrarUsuario(usuario);
            return ret == null ? NotFound("Erro ao Salvar informações") : Ok(ret);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO login)
    {
        var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == login.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha))
            return Unauthorized("E-mail ou senha inválidos");

        var token = _techChallengeService.GerarToken(usuario.Email, usuario.Role ?? "User");
        return Ok(new { Token = token });
    } 
}
