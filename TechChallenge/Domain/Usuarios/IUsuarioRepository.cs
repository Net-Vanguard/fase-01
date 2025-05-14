using TechChallenge.Infra.Data;

namespace TechChallenge.Domain.Entities.Usuario
{
    public interface IUsuarioRepository : IRepositorio<Usuario>
    {
        Task<Usuario> ObterPorEmail(string email);
        Task<bool> VerificaEmail(string email);
        Task<bool> VerificaSenha(string senha);
    }
}
