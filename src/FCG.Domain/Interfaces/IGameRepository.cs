using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task AddAsync(Game game);
        Task<Game?> GetByIdAsync(Guid id);
        Task<IEnumerable<Game>> GetAllAsync();
        void Update(Game game);
        void Delete(Game game);
        IUnitOfWork UnitOfWork { get; }
    }
}
