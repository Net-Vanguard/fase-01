using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Persistence;

public class FcgDbContext : DbContext, IUnitOfWork
{
    public FcgDbContext(DbContextOptions<FcgDbContext> options)
       : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<UserGame> UserGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Email, eb =>
            {
                eb.Property(e => e.Address)
                  .HasColumnName("Email")
                  .IsRequired();
            });

            builder.OwnsOne(u => u.Password, pb =>
            {
                // Seu VO Password expõe 'Hashed' e 'Salt', não 'Value'
                pb.Property(p => p.Hashed)
                  .HasColumnName("PasswordHash")
                  .IsRequired();

                pb.Property(p => p.Salt)
                  .HasColumnName("PasswordSalt")
                  .IsRequired();
            });

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            // Se quiser persistir PlayerIds, crie uma entidade de junção ou use Value Conversion
            // entity.Ignore(g => g.PlayerIds);
        });

        // Mapeamento da join table UserGames
        modelBuilder.Entity<UserGame>(builder =>
        {
            // Chave composta
            builder.HasKey(ug => new { ug.UserId, ug.GameId });

            // Propriedade de data
            builder.Property(ug => ug.AcquiredAt)
                   .IsRequired();

            // Foreign keys
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(ug => ug.UserId);

            builder.HasOne<Game>()
                   .WithMany()
                   .HasForeignKey(ug => ug.GameId);
        });
    }
}
