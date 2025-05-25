using FCG.Application.DTOs;

namespace FCG.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserResponse> CreateAsync(CreateUserRequest request);
        Task<UserResponse?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, UpdateUserRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
