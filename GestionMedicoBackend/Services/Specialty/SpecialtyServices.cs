using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.Specialty;
using GestionMedicoBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services.Specialty
{
    public class SpecialtyServices
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync()
        {
            return await _context.Specialties
                .Select(specialty => new SpecialtyDto
                {
                    Id = specialty.Id,
                    Nombre = specialty.Nombre,
                    Descripcion = specialty.Descripcion
                })
                .ToListAsync();
        }

        public async Task<SpecialtyDto> GetSpecialtyByIdAsync(int id)
        {
            var specialty = await _context.Specialties
                .FirstOrDefaultAsync(s => s.Id == id);

            if (specialty == null) throw new KeyNotFoundException("Especialidad no encontrada");

            return new SpecialtyDto
            {
                Id = specialty.Id,
                Nombre = specialty.Nombre,
                Descripcion = specialty.Descripcion
            };
        }

        public async Task<string> CreateSpecialtyAsync(CreateSpecialtyDto createSpecialtyDto)
        {
            var existingSpecialty = await _context.Specialties
                .FirstOrDefaultAsync(s => s.Nombre == createSpecialtyDto.Nombre);

            if (existingSpecialty != null) return "Error: La especialidad ya existe.";

            var specialty = new Models.Specialty
            {
                Nombre = createSpecialtyDto.Nombre,
                Descripcion = createSpecialtyDto.Descripcion
            };

            _context.Specialties.Add(specialty);
            await _context.SaveChangesAsync();

            return "Especialidad creada exitosamente";
        }

        public async Task<string> UpdateSpecialtyAsync(int id, UpdateSpecialtyDto updateSpecialtyDto)
        {
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty == null) return "Error: Especialidad no encontrada";

            specialty.Nombre = updateSpecialtyDto.Nombre ?? specialty.Nombre;
            specialty.Descripcion = updateSpecialtyDto.Descripcion ?? specialty.Descripcion;

            _context.Specialties.Update(specialty);
            await _context.SaveChangesAsync();
            return "Especialidad actualizada exitosamente";
        }

        public async Task<string> DeleteSpecialtyAsync(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty == null) return "Error: Especialidad no encontrada";

            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();
            return "Especialidad eliminada exitosamente";
        }
    }
}
