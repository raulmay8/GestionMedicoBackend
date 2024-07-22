using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs;
using GestionMedicoBackend.DTOs.Appointments;
using GestionMedicoBackend.Models;
using GestionMedicoBackend.Services.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AppointmentServices
{
    private readonly ApplicationDbContext _context;
    private readonly EmailServices _emailServices;

    public AppointmentServices(ApplicationDbContext context, EmailServices emailServices)
    {
        _context = context;
        _emailServices = emailServices;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
    {
        return await _context.Appointments
            .Include(a => a.Medic)
            .ThenInclude(m => m.User)
            .Include(a => a.Patient)
            .ThenInclude(p => p.User)
            .Include(a => a.Specialty)
            .Select(appointment => new AppointmentDto
            {
                Id = appointment.Id,
                Reason = appointment.Reason,
                MedicId = appointment.MedicId,
                MedicName = appointment.Medic.User.Username,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient != null ? appointment.Patient.User.Username : null,
                Nombre = appointment.Nombre,
                Apellido = appointment.Apellido,
                Genero = appointment.Genero,
                Correo = appointment.Correo,
                NumeroTelefono = appointment.NumeroTelefono,
                Estado = appointment.Estado,
                CodigoPostal = appointment.CodigoPostal,
                SpecialtyId = appointment.SpecialtyId,
                SpecialtyName = appointment.Specialty.Nombre,
                FechaCita = appointment.FechaCita
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
            .Include(a => a.Specialty)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null) throw new KeyNotFoundException("Cita no encontrada");

        return new AppointmentDto
        {
            Id = appointment.Id,
            Reason = appointment.Reason,
            MedicId = appointment.MedicId,
            MedicName = appointment.Medic.User.Username,
            PatientId = appointment.PatientId,
            PatientName = appointment.Patient != null ? appointment.Patient.User.Username : null,
            Nombre = appointment.Nombre,
            Apellido = appointment.Apellido,
            Genero = appointment.Genero,
            Correo = appointment.Correo,
            NumeroTelefono = appointment.NumeroTelefono,
            Estado = appointment.Estado,
            CodigoPostal = appointment.CodigoPostal,
            SpecialtyId = appointment.SpecialtyId,
            SpecialtyName = appointment.Specialty.Nombre,
            FechaCita = appointment.FechaCita
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

            Patient patient = null;
            if (createAppointmentDto.PatientId.HasValue)
            {
                patient = await _context.Patients
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == createAppointmentDto.PatientId.Value);
                if (patient == null) throw new KeyNotFoundException("Paciente no encontrado");

                if (medic.User.Username == patient.User.Username)
                {
                    return "Error: El médico y el paciente no pueden ser la misma persona.";
                }
            }

            var appointment = new Appointments
            {
                Reason = createAppointmentDto.Reason,
                MedicId = createAppointmentDto.MedicId,
                PatientId = createAppointmentDto.PatientId,
                Nombre = createAppointmentDto.Nombre,
                Apellido = createAppointmentDto.Apellido,
                Genero = createAppointmentDto.Genero,
                Correo = createAppointmentDto.Correo,
                NumeroTelefono = createAppointmentDto.NumeroTelefono,
                Estado = createAppointmentDto.Estado,
                CodigoPostal = createAppointmentDto.CodigoPostal,
                SpecialtyId = createAppointmentDto.SpecialtyId,
                FechaCita = createAppointmentDto.FechaCita
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Enviar correo de confirmación
            if (patient != null)
            {
                await _emailServices.SendAppointmentConfirmationEmailAsync(patient.User.Email, appointment);
            }

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
            var appointment = await _context.Appointments
                .Include(a => a.Medic)
                    .ThenInclude(m => m.User)
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null) throw new KeyNotFoundException("Cita no encontrada");

            if (updateAppointmentDto.MedicId != 0 && updateAppointmentDto.MedicId != appointment.MedicId)
            {
                var medic = await _context.Medics
                    .Include(m => m.User)
                    .FirstOrDefaultAsync(m => m.Id == updateAppointmentDto.MedicId);
                if (medic == null) throw new KeyNotFoundException("Médico no encontrado");
                appointment.MedicId = updateAppointmentDto.MedicId;
            }

            if (updateAppointmentDto.PatientId != 0 && updateAppointmentDto.PatientId != appointment.PatientId)
            {
                var patient = await _context.Patients
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == updateAppointmentDto.PatientId);
                if (patient == null) throw new KeyNotFoundException("Paciente no encontrado");
                appointment.PatientId = updateAppointmentDto.PatientId;

                // Validar que el médico y el paciente no sean la misma persona
                if (appointment.Medic.User.Username == patient.User.Username)
                {
                    return "Error: El médico y el paciente no pueden ser la misma persona.";
                }
            }

            appointment.Reason = updateAppointmentDto.Reason ?? appointment.Reason;
            appointment.Nombre = updateAppointmentDto.Nombre ?? appointment.Nombre;
            appointment.Apellido = updateAppointmentDto.Apellido ?? appointment.Apellido;
            appointment.Genero = updateAppointmentDto.Genero ?? appointment.Genero;
            appointment.Correo = updateAppointmentDto.Correo ?? appointment.Correo;
            appointment.NumeroTelefono = updateAppointmentDto.NumeroTelefono ?? appointment.NumeroTelefono;
            appointment.Estado = updateAppointmentDto.Estado ?? appointment.Estado;
            appointment.CodigoPostal = updateAppointmentDto.CodigoPostal ?? appointment.CodigoPostal;
            if (updateAppointmentDto.SpecialtyId != 0)
            {
                var specialty = await _context.Specialties.FindAsync(updateAppointmentDto.SpecialtyId);
                if (specialty == null) throw new KeyNotFoundException("Especialidad no encontrada");
                appointment.SpecialtyId = updateAppointmentDto.SpecialtyId;
            }
            appointment.FechaCita = updateAppointmentDto.FechaCita;

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
