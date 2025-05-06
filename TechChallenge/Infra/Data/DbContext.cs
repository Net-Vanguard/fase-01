using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
