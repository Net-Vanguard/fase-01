using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        void Update(User user);
        void Delete(User user);
        Task<bool> ExistsByEmailAsync(Email email);
        Task<User?> GetByEmailAsync(Email email);
        
        IUnitOfWork UnitOfWork { get; }
    }
}
