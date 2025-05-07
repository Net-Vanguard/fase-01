using TechChallenge.Domain.Common;

namespace TechChallenge.Domain.Entities
{
    public class Usuarios : RegistroBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
