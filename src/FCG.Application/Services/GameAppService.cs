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
        private readonly IUserGameRepository _userGameRepository;
        public GameAppService(IGameRepository gameRepository, IMapper mapper, IUserGameRepository userGameRepository)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _userGameRepository = userGameRepository;
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

        public async Task<bool> AcquireAsync(Guid gameId, Guid userId)
        {
            // 1. Verifica existência do jogo
            var game = await _gameRepository.GetByIdAsync(gameId);
            if (game is null)
                return false;

            // 2. Atualiza o agregado Game (se quiser manter lista de players)
            game.AddPlayer(userId);
            _gameRepository.Update(game);

            // 3. Cria a entidade de associação e persiste
            var userGame = new UserGame(userId, gameId);
            await _userGameRepository.AddAsync(userGame);

            // 4. Salva tudo numa única unidade de trabalho
            await _userGameRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
