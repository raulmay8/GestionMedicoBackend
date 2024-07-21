using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs;
using GestionMedicoBackend.DTOs.Appointments;
using GestionMedicoBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AppointmentServices
{
    private readonly ApplicationDbContext _context;

    public AppointmentServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
    {
        return await _context.Appointments
            .Include(a => a.Medic)
            .Include(a => a.Patient)
            .Select(appointment => new AppointmentDto
            {
                Id = appointment.Id,
                Reason = appointment.Reason,
                MedicId = appointment.MedicId,
                MedicName = appointment.Medic.User.Username,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient.User.Username
            })
            .ToListAsync();
    }

    public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Medic)
                .ThenInclude(m => m.User)
            .Include(a => a.Patient)
                .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null) throw new KeyNotFoundException("Cita no encontrada");

        return new AppointmentDto
        {
            Id = appointment.Id,
            Reason = appointment.Reason,
            MedicId = appointment.MedicId,
            MedicName = appointment.Medic.User.Username,
            PatientId = appointment.PatientId,
            PatientName = appointment.Patient.User.Username
        };
    }

    public async Task<string> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
    {
        try
        {
            var medic = await _context.Medics
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == createAppointmentDto.MedicId);
            if (medic == null) throw new KeyNotFoundException("Médico no encontrado");

            var patient = await _context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == createAppointmentDto.PatientId);
            if (patient == null) throw new KeyNotFoundException("Paciente no encontrado");

            if (medic.User.Username == patient.User.Username)
            {
                return "Error: El médico y el paciente no pueden ser la misma persona.";
            }

            var appointment = new Appointments
            {
                Reason = createAppointmentDto.Reason,
                MedicId = createAppointmentDto.MedicId,
                PatientId = createAppointmentDto.PatientId
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return "Cita creada con éxito.";
        }
        catch (KeyNotFoundException ex)
        {
            return $"Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }



    public async Task<string> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateAppointmentDto)
    {
        try
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) throw new KeyNotFoundException("Cita no encontrada");

            if (updateAppointmentDto.MedicId != 0 && updateAppointmentDto.MedicId != appointment.MedicId)
            {
                var medic = await _context.Medics.FindAsync(updateAppointmentDto.MedicId);
                if (medic == null) throw new KeyNotFoundException("Médico no encontrado");
                appointment.MedicId = updateAppointmentDto.MedicId;
            }

            if (updateAppointmentDto.PatientId != 0 && updateAppointmentDto.PatientId != appointment.PatientId)
            {
                var patient = await _context.Patients.FindAsync(updateAppointmentDto.PatientId);
                if (patient == null) throw new KeyNotFoundException("Paciente no encontrado");
                appointment.PatientId = updateAppointmentDto.PatientId;
            }

            appointment.Reason = updateAppointmentDto.Reason ?? appointment.Reason;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return "Cita actualizada con éxito.";
        }
        catch (KeyNotFoundException ex)
        {
            return $"Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }


    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) throw new KeyNotFoundException("Cita no encontrada");

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }
}
