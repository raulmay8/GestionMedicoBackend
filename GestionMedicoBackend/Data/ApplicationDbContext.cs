using Microsoft.EntityFrameworkCore;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Models.Consultorio;

namespace GestionMedicoBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasIndex(e => e.Value).IsUnique();
                entity.Property(e => e.Value).IsRequired();
                entity.HasOne(e => e.User)
                      .WithOne(u => u.Token)
                      .HasForeignKey<Token>(e => e.UserId);
            });
        }
    }
}
