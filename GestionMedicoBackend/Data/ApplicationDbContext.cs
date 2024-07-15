using Microsoft.EntityFrameworkCore;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Models.Consultorio;
using GestionMedicoBackend.Models.Medico;

namespace GestionMedicoBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Medico> Medicos { get; set; }

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

            modelBuilder.Entity<Consultorio>(entity =>
            {
                entity.HasKey(e => e.PkConsultorio);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Disponibilidad).IsRequired();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.PkMedico);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CorreoElectronico).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FechaDeNacimiento).IsRequired();
                entity.Property(e => e.Genero).IsRequired();
                entity.Property(e => e.CedulaProfesional).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Escuela).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AnosDeExperiencia).IsRequired();
                entity.Property(e => e.Disponibilidad).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Habilidades).IsRequired().HasMaxLength(500);
            });
        }
    }
}