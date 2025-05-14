using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infra.Data.Repository
{
    public class RepositoryBase<T> : IRepositorio<T> where T : Entitidade
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public void Adicionar(T model)
        {
            DbSet.Add(model);
        }

        public void Atualizar(T model)
        {
            DbSet.Update(model);
        }

        public void Excluir(T model)
        {
            DbSet.Remove(model);

        }
        public async Task<bool> Existe(Guid id)
        {
            return await DbSet.AnyAsync(x => x.Id == id);
        }

        public virtual async Task<T> ObterPorId(Guid Id)
        {
            return await DbSet.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
