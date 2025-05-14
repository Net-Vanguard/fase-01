using TechChallenge.Application.DTO;
using TechChallenge.Infra.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechChallenge.Domain.Entities.Usuario;
using FluentValidation;
using FluentValidation.Results;

namespace TechChallenge.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IValidator<UsuarioDTO> _validator;
        ValidationResult ValidationResult;
        public UsuarioService(ApplicationDbContext context
            , IConfiguration configuration
            , IValidator<UsuarioDTO> validator)
        {
            _context = context;
            _configuration = configuration;
            _validator = validator;
            ValidationResult = new ValidationResult();
        }

        public async Task<ValidationResult> CadastrarUsuario(UsuarioDTO usuario)
        {
            var validador = await _validator.ValidateAsync(usuario);

            if (!validador.IsValid)
            {
                AdicionaErro(validador);
                return ValidationResult;
            }

            // Hash da senha
            string senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            var salvarCadastro = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = senhaCriptografada,
            };

            _context.Usuarios.Add(salvarCadastro);
            _context.SaveChanges();

            return ValidationResult;
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
        protected void AdicionaErro(ValidationResult validation)
        {
            foreach (var error in validation.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }

    }
}

