using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Domain.Common
{
    public class RegistroBase
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public string DataCadastro { get; set; } = DateTime.UtcNow.ToString();
    }
}
