using GestionMedicoBackend.DTOs.Especialidad;
using GestionMedicoBackend.Models.Especialidad;
using GestionMedicoBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GestionMedicoBackend.Services.Especialidad
{
    public class EspecialidadService
    {
        private readonly ApplicationDbContext _context;

        public EspecialidadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EspecialidadDto>> GetAllEspecialidadesAsync()
        {
            return await _context.Especialidades
                .Select(especialidad => new EspecialidadDto
                {
                    Id = especialidad.Id,
                    Nombre = especialidad.Nombre,
                    Descripcion = especialidad.Descripcion
                })
                .ToListAsync();
        }

        public async Task<EspecialidadDto> GetEspecialidadByIdAsync(int id)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);

            if (especialidad == null)
            {
                throw new KeyNotFoundException("Especialidad no encontrada");
            }

            return new EspecialidadDto
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
                Descripcion = especialidad.Descripcion
            };
        }

        public async Task<EspecialidadDto> CreateEspecialidadAsync(CreateEspecialidadDto createEspecialidadDto)
        {
            var especialidad = new Models.Especialidad.Especialidad
            {
                Nombre = createEspecialidadDto.Nombre,
                Descripcion = createEspecialidadDto.Descripcion
            };

            _context.Especialidades.Add(especialidad);
            await _context.SaveChangesAsync();

            return new EspecialidadDto
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
                Descripcion = especialidad.Descripcion
            };
        }

        public async Task<bool> UpdateEspecialidadAsync(int id, UpdateEspecialidadDto updateEspecialidadDto)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null)
            {
                throw new KeyNotFoundException("Especialidad no encontrada");
            }

            especialidad.Nombre = updateEspecialidadDto.Nombre;
            especialidad.Descripcion = updateEspecialidadDto.Descripcion;

            _context.Especialidades.Update(especialidad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEspecialidadAsync(int id)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null)
            {
                throw new KeyNotFoundException("Especialidad no encontrada");
            }

            _context.Especialidades.Remove(especialidad);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
