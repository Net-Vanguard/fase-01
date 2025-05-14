using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.DTO;
using TechChallenge.Domain.Entities.Usuario;
using TechChallenge.Infra.Data;

namespace TechChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ApplicationDbContext _context;

    public UsuarioController(ApplicationDbContext context
        , IConfiguration configuration
        , IUsuarioService techChallengeService)
    {
        _context = context;
        _usuarioService = techChallengeService;
    }

    [HttpPost("CriarUsuario")]
    public async Task<IActionResult> CadastrarUsuario(UsuarioDTO usuario)
    {
        try
        {
            var resultado = await _usuarioService.CadastrarUsuario(usuario);
            if (!resultado.IsValid) return BadRequest(resultado.Errors);

            return Ok(resultado);
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

        var token = _usuarioService.GerarToken(usuario.Email, usuario.Role ?? "User");
        return Ok(new { Token = token });
    }
}
