using AutoMapper;
using FCG.Application.DTOs;
using FCG.Application.Services;
using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Moq;

namespace FCG.Tests.Application.Services
{
    public class UserAppServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserAppService _service;

        public UserAppServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _userRepositoryMock.SetupGet(r => r.UnitOfWork).Returns(unitOfWorkMock.Object);

            _service = new UserAppService(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddUser_AndReturnResponse()
        {
            var request = new CreateUserRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "@Password123",
                Role = EnumRole.User
            };
            var user = new User(request.Name, new Email(request.Email), new Password(request.Password), request.Role);
            var response = new UserResponse { Id = user.Id, Name = user.Name, Email = user.Email.Address };

            _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<Email>())).ReturnsAsync(false);
            _mapperMock.Setup(m => m.Map<UserResponse>(It.IsAny<User>())).Returns(response);

            var result = await _service.CreateAsync(request);

            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(response.Name, result.Name);
            Assert.Equal(response.Email, result.Email);
        }

        [Fact]
        public async Task CreateAsync_EmailAlreadyExists_ThrowsInvalidOperationException()
        {
            var request = new CreateUserRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "@Password123",
                Role = EnumRole.User
            };
            _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<Email>())).ReturnsAsync(true);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(request));
        }

        [Fact]
        public async Task GetByIdAsync_UserExists_ReturnsUserResponse()
        {
            var id = Guid.NewGuid();
            var user = new User("Test", new Email("test@example.com"), new Password("@Password123"), EnumRole.User);
            var response = new UserResponse { Id = id, Name = "Test", Email = "test@example.com" };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserResponse>(user)).Returns(response);

            var result = await _service.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(response.Name, result!.Name);
        }

        [Fact]
        public async Task GetByIdAsync_UserNotFound_ReturnsNull()
        {
            var id = Guid.NewGuid();
            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((User?)null);

            var result = await _service.GetByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedUsers()
        {
            var users = new List<User>
            {
                new User("A", new Email("a@example.com"), new Password("@Password123"), EnumRole.User),
                new User("B", new Email("b@example.com"), new Password("@Password123"), EnumRole.User)
            };
            var responses = new List<UserResponse>
            {
                new UserResponse { Id = Guid.NewGuid(), Name = "A", Email = "a@example.com" },
                new UserResponse { Id = Guid.NewGuid(), Name = "B", Email = "b@example.com" }
            };

            _userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);
            _mapperMock.Setup(m => m.Map<IEnumerable<UserResponse>>(users)).Returns(responses);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_UserExists_UpdatesAndReturnsTrue()
        {
            var id = Guid.NewGuid();
            var user = new User("Old Name", new Email("test@example.com"), new Password("@Password123"), EnumRole.User);
            var request = new UpdateUserRequest { Name = "New Name" };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(user);

            var result = await _service.UpdateAsync(id, request);

            _userRepositoryMock.Verify(r => r.Update(user), Times.Once);
            _userRepositoryMock.Verify(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_UserNotFound_ReturnsFalse()
        {
            var id = Guid.NewGuid();
            var request = new UpdateUserRequest { Name = "New Name" };
            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((User?)null);

            var result = await _service.UpdateAsync(id, request);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_UserExists_DeletesAndReturnsTrue()
        {
            var id = Guid.NewGuid();
            var user = new User("To Delete", new Email("test@example.com"), new Password("@Password123"), EnumRole.User);

            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(user);

            var result = await _service.DeleteAsync(id);

            _userRepositoryMock.Verify(r => r.Delete(user), Times.Once);
            _userRepositoryMock.Verify(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_UserNotFound_ReturnsFalse()
        {
            var id = Guid.NewGuid();
            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((User?)null);

            var result = await _service.DeleteAsync(id);

            Assert.False(result);
        }
    }
}
