using System.Text.RegularExpressions;
using TechChallenge.Application.DTO;
using TechChallenge.Domain.Entities;
using TechChallenge.Infra.Data;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TechChallenge.Application.Services
{
    public class TechChallengeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TechChallengeService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _configuration = configuration;
        }

        public string CadastrarUsuario(UsuarioDTO usuario)
        {

            try
            {
                // Validação de e-mail
                if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    return "E-mail inválido.";

                // Validação de senha (mín. 8 caracteres, letra, número e caractere especial)
                if (!Regex.IsMatch(usuario.Senha, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$"))
                    return "A senha deve ter no mínimo 8 caracteres, com letras, números e um caractere especial.";

                // Hash da senha
                string senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

                var salvarCadastro = new Usuarios
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = senhaCriptografada,

                };
                _context.Usuarios.Add(salvarCadastro);
                _context.SaveChanges();

                return "Usuario Cadastrado com sucesso";
            }
            catch (Exception ex)
            {
                return $"Erro {ex} usuario não cadastrado";
            }

        }

        public string GerarToken(string email, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

