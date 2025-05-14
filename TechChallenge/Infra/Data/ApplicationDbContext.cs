using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities.Usuario;

namespace TechChallenge.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
