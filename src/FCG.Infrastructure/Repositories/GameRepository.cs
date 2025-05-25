using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly FcgDbContext _context;

    public GameRepository(FcgDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Game game)
    {
        await _context.Games.AddAsync(game);
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await _context.Games.FindAsync(id);
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _context.Games.ToListAsync();
    }

    public void Update(Game game)
    {
        _context.Games.Update(game);
    }

    public void Delete(Game game)
    {
        _context.Games.Remove(game);
    }

    public IUnitOfWork UnitOfWork => _context;
}