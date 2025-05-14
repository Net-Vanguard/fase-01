using System.Linq.Expressions;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infra.Data
{
    public interface IRepositorio<T> : IDisposable where T : Entitidade
    {
        void Adicionar(T model);
        void Atualizar(T model);
        void Excluir(T model);
        Task<T> ObterPorId(Guid Id);
        Task<bool> Existe(Guid Id);
        Task<IEnumerable<T>> ObterTodos();
    }
}
