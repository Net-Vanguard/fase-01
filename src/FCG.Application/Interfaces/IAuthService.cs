using FCG.Application.DTOs;

namespace FCG.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateAsync(LoginRequest request);
    }
}
