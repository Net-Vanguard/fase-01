using AutoMapper;
using FCG.Application.DTOs;
using FCG.Application.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserAppService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest request)
        {
            // Verifica se o e-mail já está cadastrado
            var email = new Email(request.Email);
            if (await _userRepository.ExistsByEmailAsync(email))
            {
                throw new InvalidOperationException("E-mail já cadastrado.");
            }

            // Mapeia e cria entidade de domínio
            var user = new User(
                request.Name,
                email,
                new Password(request.Password),
                request.Role
            );

            await _userRepository.AddAsync(user);
            await _userRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user is null ? null : _mapper.Map<UserResponse>(user);
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return false;

            user.ChangeName(request.Name);

            _userRepository.Update(user);
            await _userRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return false;

            _userRepository.Delete(user);
            await _userRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
