using FCG.Domain.Enums;

namespace FCG.Application.DTOs
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public EnumRole Role { get; set; } = EnumRole.User; 

    }
}
