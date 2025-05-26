using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infrastructure.Persistence;

namespace FCG.Infrastructure.Repositories
{
    public class UserGameRepository : IUserGameRepository
    {
        private readonly FcgDbContext _context;
        public UserGameRepository(FcgDbContext context) => _context = context;

        public async Task AddAsync(UserGame userGame)
        {
            await _context.UserGames.AddAsync(userGame);
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
