using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.Horario;
using GestionMedicoBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services.Horario
{
    public class HorarioServices
    {
        private readonly ApplicationDbContext _context;

        public HorarioServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HorarioDto>> GetAllHorariosAsync()
        {
            return await _context.Horarios
                .Include(h => h.Medics)
                .Select(horario => new HorarioDto
                {
                    Id = horario.Id,
                    Name = horario.Name,
                    Fecha = horario.Fecha,
                    Turno = horario.Turno,
                    Entrada = horario.Entrada.ToTimeSpan(),
                    Salida = horario.Salida.ToTimeSpan()
                })
                .ToListAsync();
        }

        public async Task<HorarioDto> GetHorarioByIdAsync(int id)
        {
            var horario = await _context.Horarios
                .Include(h => h.Medics)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (horario == null) throw new KeyNotFoundException("Horario no encontrado");

            return new HorarioDto
            {
                Id = horario.Id,
                Name = horario.Name,
                Fecha = horario.Fecha,
                Turno = horario.Turno,
                Entrada = horario.Entrada.ToTimeSpan(),
                Salida = horario.Salida.ToTimeSpan()
            };
        }

        public async Task<string> CreateHorarioAsync(CreateHorarioDto createHorarioDto)
        {
            var horario = new Models.Horario
            {
                Name = createHorarioDto.Name,
                Fecha = createHorarioDto.Fecha,
                Turno = createHorarioDto.Turno,
                Entrada = TimeOnly.FromTimeSpan(createHorarioDto.Entrada),
                Salida = TimeOnly.FromTimeSpan(createHorarioDto.Salida)
            };

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return "Horario creado exitosamente";
        }

        public async Task<bool> UpdateHorarioAsync(int id, UpdateHorarioDto updateHorarioDto)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) throw new KeyNotFoundException("Horario no encontrado");

            horario.Name = updateHorarioDto.Name ?? horario.Name;
            horario.Fecha = updateHorarioDto.Fecha != default ? updateHorarioDto.Fecha : horario.Fecha;
            horario.Turno = updateHorarioDto.Turno ?? horario.Turno;
            horario.Entrada = updateHorarioDto.Entrada != default ? TimeOnly.FromTimeSpan(updateHorarioDto.Entrada) : horario.Entrada;
            horario.Salida = updateHorarioDto.Salida != default ? TimeOnly.FromTimeSpan(updateHorarioDto.Salida) : horario.Salida;

            _context.Horarios.Update(horario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHorarioAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) throw new KeyNotFoundException("Horario no encontrado");

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
