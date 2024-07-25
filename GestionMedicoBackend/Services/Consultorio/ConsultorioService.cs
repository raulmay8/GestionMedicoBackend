using GestionMedicoBackend.DTOs.Consultorio;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            .Include(c => c.Medics)
                .ThenInclude(m => m.User)
            .Select(consultorio => new ConsultorioDto
            {
                Id = consultorio.Id,
                Name = consultorio.Name,
                Status = consultorio.Status,
                Availability = consultorio.Availability,
                MedicNames = consultorio.Medics.Select(m => m.User.Username).ToList()
            })
            .ToListAsync();
    }

    public async Task<ConsultorioDto> GetConsultorioByIdAsync(int id)
    {
        var consultorio = await _context.Consultorios
            .Include(c => c.Medics)
                .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (consultorio == null) throw new KeyNotFoundException("Consultorio no encontrado");

        return new ConsultorioDto
        {
            Id = consultorio.Id,
            Name = consultorio.Name,
            Status = consultorio.Status,
            Availability = consultorio.Availability,
            MedicNames = consultorio.Medics.Select(m => m.User.Username).ToList()
        };
    }

    public async Task<ConsultorioDto> CreateConsultorioAsync(CreateConsultorioDto createConsultorioDto)
    {
        if (await _context.Consultorios.AnyAsync(c => c.Name == createConsultorioDto.Name))
        {
            throw new Exception("El nombre del consultorio ya está en uso.");
        }

        var consultorio = new Consultorio
        {
            Name = createConsultorioDto.Name,
            Status = true,
            Availability = true
        };

        _context.Consultorios.Add(consultorio);
        await _context.SaveChangesAsync();

        return new ConsultorioDto
        {
            Id = consultorio.Id,
            Name = consultorio.Name,
            Status = consultorio.Status,
            Availability = consultorio.Availability,
            MedicNames = new List<string>()
        };
    }

    public async Task<bool> UpdateConsultorioAsync(int id, UpdateConsultorioDto updateConsultorioDto)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null)
        {
            throw new KeyNotFoundException("Consultorio no encontrado");
        }

        if (consultorio.Name != updateConsultorioDto.Name && await _context.Consultorios.AnyAsync(c => c.Name == updateConsultorioDto.Name))
        {
            throw new Exception("El nombre del consultorio ya está en uso.");
        }

        consultorio.Name = updateConsultorioDto.Name ?? consultorio.Name;

        _context.Consultorios.Update(consultorio);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteConsultorioAsync(int id)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null) throw new KeyNotFoundException("Consultorio no encontrado");

        _context.Consultorios.Remove(consultorio);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleConsultorioStatusAsync(int id)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null) throw new KeyNotFoundException("Consultorio no encontrado");

        consultorio.Status = !consultorio.Status;

        _context.Consultorios.Update(consultorio);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ToggleConsultorioAvailabilityAsync(int id)
    {
        var consultorio = await _context.Consultorios.FindAsync(id);
        if (consultorio == null) throw new KeyNotFoundException("Consultorio no encontrado");

        consultorio.Availability = !consultorio.Availability;

        _context.Consultorios.Update(consultorio);
        await _context.SaveChangesAsync();
        return true;
    }

}
