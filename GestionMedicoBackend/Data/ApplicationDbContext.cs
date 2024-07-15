using Microsoft.EntityFrameworkCore;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Models.Auth;

namespace GestionMedicoBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(e => e.RoleId);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasIndex(e => e.Value).IsUnique();
                entity.Property(e => e.Value).IsRequired();
                entity.HasOne(e => e.User)
                      .WithOne(u => u.Token)
                      .HasForeignKey<Token>(e => e.UserId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(rp => rp.Id);

                entity.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(rp => rp.RoleId);

                entity.HasOne(rp => rp.Permission)
                      .WithMany(p => p.RolePermissions)
                      .HasForeignKey(rp => rp.PermissionId);
            });
        }
    }
}
