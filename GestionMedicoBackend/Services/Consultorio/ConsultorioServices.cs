using GestionMedicoBackend.DTOs.Consultorio;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using GestionMedicoBackend.Models.Consultorio;

public class ConsultorioService
{
    private readonly ApplicationDbContext _context;

    public ConsultorioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ConsultorioDto>> GetAllConsultoriosAsync()
    {
        return await _context.Consultorios
            .Select(consultorio => new ConsultorioDto
            {
                PkConsultorio = consultorio.PkConsultorio,
                Nombre = consultorio.Nombre,
                Disponibilidad = consultorio.Disponibilidad,
                Status = consultorio.Status
            })
            .ToListAsync();
    }

    public async Task<ConsultorioDto> GetConsultorioByIdAsync(int id)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null) return null;

        return new ConsultorioDto
        {
            PkConsultorio = consultorio.PkConsultorio,
            Nombre = consultorio.Nombre,
            Disponibilidad = consultorio.Disponibilidad,
            Status = consultorio.Status
        };
    }

    public async Task<ConsultorioDto> CreateConsultorioAsync(CreateConsultorioDto createConsultorioDto)
    {
        var consultorio = new Consultorio
        {
            Nombre = createConsultorioDto.Nombre,
            Disponibilidad = createConsultorioDto.Disponibilidad,
            Status = createConsultorioDto.Status
        };

        _context.Consultorios.Add(consultorio);
        await _context.SaveChangesAsync();

        return new ConsultorioDto
        {
            PkConsultorio = consultorio.PkConsultorio,
            Nombre = consultorio.Nombre,
            Disponibilidad = consultorio.Disponibilidad,
            Status = consultorio.Status
        };
    }

    public async Task<bool> UpdateConsultorioAsync(int id, UpdateConsultorioDto updateConsultorioDto)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null) return false;

        consultorio.Nombre = updateConsultorioDto.Nombre;
        consultorio.Disponibilidad = updateConsultorioDto.Disponibilidad;
        consultorio.Status = updateConsultorioDto.Status;

        _context.Consultorios.Update(consultorio);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteConsultorioAsync(int id)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null) return false;

        _context.Consultorios.Remove(consultorio);
        await _context.SaveChangesAsync();
        return true;
    }
}
