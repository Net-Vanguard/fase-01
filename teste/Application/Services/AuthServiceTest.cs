using FCG.Application.DTOs;
using FCG.Application.Services;
using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FCG.Tests.Application.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);

            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("super_secret_key_1234567890123456");
            _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
            _configurationMock.Setup(c => c["Jwt:ExpiresMinutes"]).Returns("60");
        }

        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsAuthResponseWithToken()
        {
            // Arrange
            var email = "user@example.com";
            var passwordPlain = "@Password123";
            var loginRequest = new LoginRequest { Email = email, Password = passwordPlain };

            var emailVo = new Email(email);
            var passwordVo = new Password(passwordPlain);

            var user = new User("Test User", emailVo, passwordVo, EnumRole.User);

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(It.Is<Email>(e => e.Address == email)))
                .ReturnsAsync(user);

            // Act
            var result = await _authService.AuthenticateAsync(loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Token));
            Assert.Equal("Test User", result.Username);
            Assert.True(result.ExpiresAt > DateTime.UtcNow);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidEmail_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "notfound@example.com",
                Password = "irrelevant"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(It.IsAny<Email>()))
                .ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _authService.AuthenticateAsync(loginRequest)
            );
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidPassword_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var email = "user@example.com";
            var correctPlain = "@Password123";
            var wrongPlain = "wrongpassword";

            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = wrongPlain
            };

            var emailVo = new Email(email);
            var passwordVo = new Password(correctPlain);

            var user = new User("Test User", emailVo, passwordVo, EnumRole.User);

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(It.Is<Email>(e => e.Address == email)))
                .ReturnsAsync(user);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _authService.AuthenticateAsync(loginRequest)
            );
        }
    }
}
