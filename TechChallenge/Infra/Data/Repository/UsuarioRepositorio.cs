using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities.Usuario;

namespace TechChallenge.Infra.Data.Repository
{
    public class UsuarioRepositorio : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepositorio(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Usuario> ObterPorEmail(string email)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<bool> VerificaEmail(string email)
        {
            return await DbSet.AnyAsync(x => x.Email == email);
        }
        public async Task<bool> VerificaSenha(string senha)
        {
            return await DbSet.AnyAsync(x => x.Senha == senha);
        }
    }
}
