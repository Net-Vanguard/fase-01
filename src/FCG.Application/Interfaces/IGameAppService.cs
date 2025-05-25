using FCG.Application.DTOs;

namespace FCG.Application.Interfaces
{
    public interface IGameAppService
    {
        Task<GameResponse> CreateAsync(CreateGameRequest request);
        Task<GameResponse?> GetByIdAsync(Guid id);
        Task<IEnumerable<GameResponse>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, UpdateGameRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
