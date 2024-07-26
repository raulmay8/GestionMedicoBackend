using Microsoft.EntityFrameworkCore;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Models.Auth;
using GestionMedicoBackend.Models.Contact;

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
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<HistorialClinico> HistorialClinicos { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

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

                entity.HasMany(e => e.Patients)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Medics)
                      .WithOne(m => m.User)
                      .HasForeignKey(m => m.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
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

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Occupation).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Picture).IsRequired().HasMaxLength(50);
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Patients)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Medic>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.ProfessionalId).IsRequired().HasMaxLength(50);
                entity.Property(m => m.School).IsRequired().HasMaxLength(50);
                entity.Property(m => m.YearExperience).IsRequired();
                entity.Property(m => m.DateGraduate).IsRequired();
                entity.Property(m => m.Availability).IsRequired();
                entity.HasOne(m => m.User)
                      .WithMany(u => u.Medics)
                      .HasForeignKey(m => m.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Horario)
                       .WithMany(h => h.Medics)
                       .HasForeignKey(m => m.HorarioId)
                       .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Consultorio)
                      .WithMany(u => u.Medics)
                      .HasForeignKey(m => m.ConsultorioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Reason).IsRequired().HasColumnType("text");
                entity.Property(a => a.Nombre).HasMaxLength(100);
                entity.Property(a => a.Apellido).HasMaxLength(100);
                entity.Property(a => a.Genero).HasMaxLength(20);
                entity.Property(a => a.Correo).HasMaxLength(255);
                entity.Property(a => a.NumeroTelefono).HasMaxLength(15);
                entity.Property(a => a.Estado).HasMaxLength(100);
                entity.Property(a => a.CodigoPostal).HasMaxLength(10);
                entity.Property(a => a.FechaCita).IsRequired();

                entity.HasOne(a => a.Patient)
                      .WithMany(p => p.Appointments)
                      .HasForeignKey(a => a.PatientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Medic)
                      .WithMany(m => m.Appointments)
                      .HasForeignKey(a => a.MedicId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Specialty)
                      .WithMany(s => s.Appointments)
                      .HasForeignKey(a => a.SpecialtyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Descripcion).IsRequired().HasColumnType("text");
            });

            modelBuilder.Entity<Consultorio>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).HasMaxLength(100);
            });
            modelBuilder.Entity<HistorialClinico>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasOne(a => a.Patient)
                      .WithOne(u => u.HistorialClinico)
                      .HasForeignKey<HistorialClinico>(a => a.PatientId);
            });
            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(500);
            });
        }
    }
}
