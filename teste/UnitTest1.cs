using Xunit;
using Moq;
using AutoMapper;
using FluentAssertions;
using System.Threading.Tasks;
using System;
using FCG.Application.Services;
using FCG.Application.DTOs;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;

namespace FCG.Application.Tests
{
    public class GameAppServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarGameResponse_QuandoSucesso()
        {
            var fakeGame = new Game("Jogo Teste");
            var request = new CreateGameRequest { Title = "Jogo Teste" };
            var expectedResponse = new GameResponse { Id = Guid.NewGuid(), Title = "Jogo Teste" };

            var gameRepoMock = new Mock<IGameRepository>();
            gameRepoMock.Setup(r => r.AddAsync(It.IsAny<Game>())).Returns(Task.CompletedTask);
            gameRepoMock.SetupGet(r => r.UnitOfWork).Returns(Mock.Of<IUnitOfWork>());

            var userGameRepoMock = new Mock<IUserGameRepository>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<GameResponse>(It.IsAny<Game>())).Returns(expectedResponse);

            var service = new GameAppService(
                gameRepoMock.Object,
                mapperMock.Object,
                userGameRepoMock.Object
            );

            // Act
            var resultado = await service.CreateAsync(request);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Title.Should().Be("Jogo Teste");
        }

        [Fact]
        public async Task AcquireAsync_DeveRetornarTrue_QuandoJogoExiste()
        {
            var gameId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var fakeGame = new Game("Jogo Teste");

            var gameRepoMock = new Mock<IGameRepository>();
            gameRepoMock.Setup(r => r.GetByIdAsync(gameId)).ReturnsAsync(fakeGame);
            gameRepoMock.Setup(r => r.Update(It.IsAny<Game>()));
            gameRepoMock.SetupGet(r => r.UnitOfWork).Returns(Mock.Of<IUnitOfWork>());

            var userGameRepoMock = new Mock<IUserGameRepository>();
            userGameRepoMock.Setup(r => r.AddAsync(It.IsAny<UserGame>())).Returns(Task.CompletedTask);
            userGameRepoMock.SetupGet(r => r.UnitOfWork).Returns(Mock.Of<IUnitOfWork>());

            var mapperMock = new Mock<IMapper>();

            var service = new GameAppService(
                gameRepoMock.Object,
                mapperMock.Object,
                userGameRepoMock.Object
            );

            var resultado = await service.AcquireAsync(gameId, userId);

            resultado.Should().BeTrue();
        }


    }
}
