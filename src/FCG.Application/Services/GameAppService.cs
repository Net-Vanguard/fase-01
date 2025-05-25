using AutoMapper;
using FCG.Application.DTOs;
using FCG.Application.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;

namespace FCG.Application.Services
{
    public class GameAppService : IGameAppService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameAppService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GameResponse> CreateAsync(CreateGameRequest request)
        {
            var game = new Game(request.Title);
            await _gameRepository.AddAsync(game);
            await _gameRepository.UnitOfWork.SaveChangesAsync();
            return _mapper.Map<GameResponse>(game);
        }

        public async Task<GameResponse?> GetByIdAsync(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return game is null
                ? null
                : _mapper.Map<GameResponse>(game);
        }

        public async Task<IEnumerable<GameResponse>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GameResponse>>(games);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateGameRequest request)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game is null) return false;

            game.ChangeTitle(request.Title);
            _gameRepository.Update(game);
            await _gameRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game is null) return false;

            _gameRepository.Delete(game);
            await _gameRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
