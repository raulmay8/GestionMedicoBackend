/*using GestionMedicoBackend.DTOs.Horario;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionMedicoBackend.Models.Horario;

namespace GestionMedicoBackend.Services
{
    public class HorarioService
    {
        private readonly ApplicationDbContext _context;

        public HorarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HorarioDto>> GetAllHorariosAsync()
        {
            return await _context.Horarios
                .Select(horario => new HorarioDto
                {
                    Id = horario.Id,
                    ClinicaOConsultorio = horario.ClinicaOConsultorio,
                    Medico = horario.Medico,
                    Fecha = horario.Fecha,
                    Turno = horario.Turno,
                    Entrada = horario.Entrada,
                    Salida = horario.Salida
                })
                .ToListAsync();
        }

        public async Task<HorarioDto> GetHorarioByIdAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) return null;

            return new HorarioDto
            {
                Id = horario.Id,
                ClinicaOConsultorio = horario.ClinicaOConsultorio,
                Medico = horario.Medico,
                Fecha = horario.Fecha,
                Turno = horario.Turno,
                Entrada = horario.Entrada,
                Salida = horario.Salida
            };
        }

        public async Task<HorarioDto> CreateHorarioAsync(CreateHorarioDto createHorarioDto)
        {
            var horario = new Horario
            {
                ClinicaOConsultorio = createHorarioDto.ClinicaOConsultorio,
                Medico = createHorarioDto.Medico,
                Fecha = createHorarioDto.Fecha,
                Turno = createHorarioDto.Turno,
                Entrada = createHorarioDto.Entrada,
                Salida = createHorarioDto.Salida
            };

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return new HorarioDto
            {
                Id = horario.Id,
                ClinicaOConsultorio = horario.ClinicaOConsultorio,
                Medico = horario.Medico,
                Fecha = horario.Fecha,
                Turno = horario.Turno,
                Entrada = horario.Entrada,
                Salida = horario.Salida
            };
        }

        public async Task<bool> UpdateHorarioAsync(int id, UpdateHorarioDto updateHorarioDto)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) return false;

            horario.ClinicaOConsultorio = updateHorarioDto.ClinicaOConsultorio;
            horario.Medico = updateHorarioDto.Medico;
            horario.Fecha = updateHorarioDto.Fecha;
            horario.Turno = updateHorarioDto.Turno;
            horario.Entrada = updateHorarioDto.Entrada;
            horario.Salida = updateHorarioDto.Salida;

            _context.Horarios.Update(horario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHorarioAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) return false;

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
*/