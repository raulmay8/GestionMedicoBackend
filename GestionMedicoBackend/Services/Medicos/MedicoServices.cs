using GestionMedicoBackend.DTOs.Medico;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using GestionMedicoBackend.Models.Medico;

public class MedicoService
{
    private readonly ApplicationDbContext _context;

    public MedicoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicoDto>> GetAllMedicosAsync()
    {
        return await _context.Medicos
            .Select(medico => new MedicoDto
            {
                PkMedico = medico.PkMedico,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                CorreoElectronico = medico.CorreoElectronico,
                Telefono = medico.Telefono,
                FechaDeNacimiento = medico.FechaDeNacimiento,
                Genero = medico.Genero,
                CedulaProfesional = medico.CedulaProfesional,
                Escuela = medico.Escuela,
                AnosDeExperiencia = medico.AnosDeExperiencia,
                Disponibilidad = medico.Disponibilidad,
                Status = medico.Status,
                Habilidades = medico.Habilidades
            })
            .ToListAsync();
    }

    public async Task<MedicoDto> GetMedicoByIdAsync(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null) return null;

        return new MedicoDto
        {
            PkMedico = medico.PkMedico,
            Nombre = medico.Nombre,
            Apellido = medico.Apellido,
            CorreoElectronico = medico.CorreoElectronico,
            Telefono = medico.Telefono,
            FechaDeNacimiento = medico.FechaDeNacimiento,
            Genero = medico.Genero,
            CedulaProfesional = medico.CedulaProfesional,
            Escuela = medico.Escuela,
            AnosDeExperiencia = medico.AnosDeExperiencia,
            Disponibilidad = medico.Disponibilidad,
            Status = medico.Status,
            Habilidades = medico.Habilidades
        };
    }

    public async Task<MedicoDto> CreateMedicoAsync(CreateMedicoDto createMedicoDto)
    {
        var medico = new Medico
        {
            Nombre = createMedicoDto.Nombre,
            Apellido = createMedicoDto.Apellido,
            CorreoElectronico = createMedicoDto.CorreoElectronico,
            Password = createMedicoDto.Password,
            Telefono = createMedicoDto.Telefono,
            FechaDeNacimiento = createMedicoDto.FechaDeNacimiento,
            Genero = createMedicoDto.Genero,
            CedulaProfesional = createMedicoDto.CedulaProfesional,
            Escuela = createMedicoDto.Escuela,
            AnosDeExperiencia = createMedicoDto.AnosDeExperiencia,
            Disponibilidad = createMedicoDto.Disponibilidad,
            Status = createMedicoDto.Status,
            Habilidades = createMedicoDto.Habilidades
        };

        _context.Medicos.Add(medico);
        await _context.SaveChangesAsync();

        return new MedicoDto
        {
            PkMedico = medico.PkMedico,
            Nombre = medico.Nombre,
            Apellido = medico.Apellido,
            CorreoElectronico = medico.CorreoElectronico,
            Telefono = medico.Telefono,
            FechaDeNacimiento = medico.FechaDeNacimiento,
            Genero = medico.Genero,
            CedulaProfesional = medico.CedulaProfesional,
            Escuela = medico.Escuela,
            AnosDeExperiencia = medico.AnosDeExperiencia,
            Disponibilidad = medico.Disponibilidad,
            Status = medico.Status,
            Habilidades = medico.Habilidades
        };
    }

    public async Task<bool> UpdateMedicoAsync(int id, UpdateMedicoDto updateMedicoDto)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null) return false;

        medico.Nombre = updateMedicoDto.Nombre;
        medico.Apellido = updateMedicoDto.Apellido;
        medico.CorreoElectronico = updateMedicoDto.CorreoElectronico;
        medico.Telefono = updateMedicoDto.Telefono;
        medico.FechaDeNacimiento = updateMedicoDto.FechaDeNacimiento;
        medico.Genero = updateMedicoDto.Genero;
        medico.CedulaProfesional = updateMedicoDto.CedulaProfesional;
        medico.Escuela = updateMedicoDto.Escuela;
        medico.AnosDeExperiencia = updateMedicoDto.AnosDeExperiencia;
        medico.Disponibilidad = updateMedicoDto.Disponibilidad;
        medico.Status = updateMedicoDto.Status;
        medico.Habilidades = updateMedicoDto.Habilidades;

        _context.Medicos.Update(medico);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMedicoAsync(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null) return false;

        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();
        return true;
    }
}
