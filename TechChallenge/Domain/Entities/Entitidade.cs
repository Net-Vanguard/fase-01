using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Domain.Entities
{
    public class Entitidade
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DataCadastro { get; set; } = DateTime.UtcNow.ToString();

    }
}
