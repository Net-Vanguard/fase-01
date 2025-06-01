using AutoMapper;
using FCG.Application.DTOs;
using FCG.Application.Services;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Moq;

namespace FCG.Tests.Application.Services
{
    public class GameAppServiceTest
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IUserGameRepository> _userGameRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GameAppService _service;

        public GameAppServiceTest()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _userGameRepositoryMock = new Mock<IUserGameRepository>();
            _mapperMock = new Mock<IMapper>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _gameRepositoryMock.SetupGet(r => r.UnitOfWork).Returns(unitOfWorkMock.Object);
            _userGameRepositoryMock.SetupGet(r => r.UnitOfWork).Returns(unitOfWorkMock.Object);

            _service = new GameAppService(_gameRepositoryMock.Object, _mapperMock.Object, _userGameRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddGame_AndReturnResponse()
        {
            var request = new CreateGameRequest { Title = "Test Game" };
            var game = new Game(request.Title);
            var response = new GameResponse { Id = game.Id, Title = game.Title, PlayerIds = new List<Guid>() };

            _mapperMock.Setup(m => m.Map<GameResponse>(It.IsAny<Game>())).Returns(response);

            var result = await _service.CreateAsync(request);

            _gameRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Game>()), Times.Once);
            _gameRepositoryMock.Verify(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(response.Title, result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_GameExists_ReturnsGameResponse()
        {
            var id = Guid.NewGuid();
            var game = new Game("Test") { };
            var response = new GameResponse { Id = id, Title = "Test", PlayerIds = new List<Guid>() };

            _gameRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(game);
            _mapperMock.Setup(m => m.Map<GameResponse>(game)).Returns(response);

            var result = await _service.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(response.Title, result!.Title);
        }

        [Fact]
        public async Task GetByIdAsync_GameNotFound_ReturnsNull()
        {
            var id = Guid.NewGuid();
            _gameRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Game?)null);

            var result = await _service.GetByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedGames()
        {
            var games = new List<Game> { new Game("A"), new Game("B") };
            var responses = new List<GameResponse>
            {
                new GameResponse { Id = Guid.NewGuid(), Title = "A", PlayerIds = new List<Guid>() },
                new GameResponse { Id = Guid.NewGuid(), Title = "B", PlayerIds = new List<Guid>() }
            };

            _gameRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(games);
            _mapperMock.Setup(m => m.Map<IEnumerable<GameResponse>>(games)).Returns(responses);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_GameExists_UpdatesAndReturnsTrue()
        {
            var id = Guid.NewGuid();
            var game = new Game("Old Title");
            var request = new UpdateGameRequest { Title = "New Title" };

            _gameRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(game);

            var result = await _service.UpdateAsync(id, request);

            _gameRepositoryMock.Verify(r => r.Update(game), Times.Once);
            _gameRepositoryMock.Verify(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_GameNotFound_ReturnsFalse()
        {
            var id = Guid.NewGuid();
            var request = new UpdateGameRequest { Title = "New Title" };
            _gameRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Game?)null);

            var result = await _service.UpdateAsync(id, request);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_GameExists_DeletesAndReturnsTrue()
        {
            var id = Guid.NewGuid();
            var game = new Game("To Delete");

            _gameRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(game);

            var result = await _service.DeleteAsync(id);

            _gameRepositoryMock.Verify(r => r.Delete(game), Times.Once);
            _gameRepositoryMock.Verify(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_GameNotFound_ReturnsFalse()
        {
            var id = Guid.NewGuid();
            _gameRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Game?)null);

            var result = await _service.DeleteAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task AcquireAsync_GameExists_AddsPlayerAndUserGame_ReturnsTrue()
        {
            var gameId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var game = new Game("Acquire");

            _gameRepositoryMock.Setup(r => r.GetByIdAsync(gameId)).ReturnsAsync(game);

            var result = await _service.AcquireAsync(gameId, userId);

            _gameRepositoryMock.Verify(r => r.Update(game), Times.Once);
            _userGameRepositoryMock.Verify(r => r.AddAsync(It.Is<UserGame>(ug => ug.UserId == userId && ug.GameId == gameId)), Times.Once);
            _userGameRepositoryMock.Verify(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task AcquireAsync_GameNotFound_ReturnsFalse()
        {
            var gameId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            _gameRepositoryMock.Setup(r => r.GetByIdAsync(gameId)).ReturnsAsync((Game?)null);

            var result = await _service.AcquireAsync(gameId, userId);

            Assert.False(result);
        }
    }
}