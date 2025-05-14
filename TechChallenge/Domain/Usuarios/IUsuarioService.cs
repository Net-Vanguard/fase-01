using FluentValidation.Results;
using TechChallenge.Application.DTO;

namespace TechChallenge.Domain.Entities.Usuario
{
    public interface IUsuarioService
    {
        Task<ValidationResult> CadastrarUsuario(UsuarioDTO usuario);
        string GerarToken(string email, string role);
    }
}

