using TechChallenge.Application.DTO;
using TechChallenge.Domain.Entities.Usuario;

namespace TechChallenge.Domain.Entities.Usuario
{
    public class Usuario : Entitidade
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public EnumPerfil Perfil { get; set; }
    }
}
