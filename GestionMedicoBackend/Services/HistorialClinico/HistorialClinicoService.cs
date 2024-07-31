using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.HistorialClinico;
using GestionMedicoBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services.HistorialClinico
{
    public class HistorialClinicoServices
    {
        private readonly ApplicationDbContext _context;

        public HistorialClinicoServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistorialClinicoDto>> GetAllHistorialClinicoAsync()
        {
            return await _context.HistorialClinicos
                .Include(h => h.Patient)
                .Select(historial => new HistorialClinicoDto
                {
                    Id = historial.Id,
                    Name = historial.Name,
                    Surname = historial.Surname,
                    Address = historial.Address,
                    Gender = historial.Gender,
                    Email = historial.Email,
                    Phone = historial.Phone,
                    State = historial.State,
                    PostalCode = historial.PostalCode,
                    Smoke = historial.Smoke,
                    Alcohol = historial.Alcohol,
                    Coffee = historial.Coffee,
                    Allergic = historial.Allergic,
                    Allergies = historial.Allergies,
                    TakesMedication = historial.TakesMedication,
                    Medication = historial.Medication,
                    MedicalHistory = historial.MedicalHistory,
                    PatientId = historial.PatientId,
                    PatientName = historial.Patient.User.Username
                })
                .ToListAsync();
        }

        public async Task<HistorialClinicoDto> GetHistorialClinicoByIdAsync(int id)
        {
            var historial = await _context.HistorialClinicos
                .Include(h => h.Patient)
                .FirstOrDefaultAsync(h => h.PatientId == id);

            if (historial == null) throw new KeyNotFoundException("Historial clínico no encontrado");
             HistorialClinicoDto hist = new HistorialClinicoDto();
            hist.Id = id;
            hist.Name = historial.Name;
            hist.Phone = historial.Phone;
            hist.Email = historial.Email; 
            hist.Address = historial.Address;  
            hist.Coffee = historial.Coffee;
            hist.Allergic = historial.Allergic;
            hist.Smoke = historial.Smoke;
            hist.Surname = historial.Surname;
            hist.Name = historial.Name;
            hist.Gender = historial.Gender;
            hist.PostalCode = historial.PostalCode; 
            hist.State = historial.State;
            hist.Smoke = historial.Smoke;
            hist.Allergies = historial.Allergies;
            hist.TakesMedication = historial.TakesMedication;
            hist.Medication = historial.Medication;
            hist.MedicalHistory = historial.MedicalHistory;
            hist.PatientId = historial.PatientId;
            
            return hist;
    
        }

        public async Task<string> CreateHistorialClinicoAsync(CreateHistorialClinicoDto createHistorialClinicoDto)
        {
            var patient = await _context.Patients.FindAsync(createHistorialClinicoDto.PatientId);
            if (patient == null) return "Error: Paciente no encontrado";

            var historial = new Models.HistorialClinico
            {
                Name = createHistorialClinicoDto.Name,
                Surname = createHistorialClinicoDto.Surname,
                Address = createHistorialClinicoDto.Address,
                Gender = createHistorialClinicoDto.Gender,
                Email = createHistorialClinicoDto.Email,
                Phone = createHistorialClinicoDto.Phone,
                State = createHistorialClinicoDto.State,
                PostalCode = createHistorialClinicoDto.PostalCode,
                Smoke = createHistorialClinicoDto.Smoke,
                Alcohol = createHistorialClinicoDto.Alcohol,
                Coffee = createHistorialClinicoDto.Coffee,
                Allergic = createHistorialClinicoDto.Allergic,
                Allergies = createHistorialClinicoDto.Allergies,
                TakesMedication = createHistorialClinicoDto.TakesMedication,
                Medication = createHistorialClinicoDto.Medication,
                MedicalHistory = createHistorialClinicoDto.MedicalHistory,
                PatientId = createHistorialClinicoDto.PatientId
            };

            _context.HistorialClinicos.Add(historial);
            await _context.SaveChangesAsync();

            return "Historial clínico creado exitosamente";
        }

        public async Task<bool> UpdateHistorialClinicoAsync(int id, UpdateHistorialClinicoDto updateHistorialClinicoDto)
        {
            var historial = await _context.HistorialClinicos.FirstOrDefaultAsync(h => h.PatientId == id);
            if (historial == null) throw new KeyNotFoundException("Historial clínico no encontrado");

            historial.Name = updateHistorialClinicoDto.Name ?? historial.Name;
            historial.Surname = updateHistorialClinicoDto.Surname ?? historial.Surname;
            historial.Address = updateHistorialClinicoDto.Address ?? historial.Address;
            historial.Gender = updateHistorialClinicoDto.Gender ?? historial.Gender;
            historial.Email = updateHistorialClinicoDto.Email ?? historial.Email;
            historial.Phone = updateHistorialClinicoDto.Phone ?? historial.Phone;
            historial.State = updateHistorialClinicoDto.State ?? historial.State;
            historial.PostalCode = updateHistorialClinicoDto.PostalCode ?? historial.PostalCode;
            historial.Smoke = updateHistorialClinicoDto.Smoke ?? historial.Smoke;
            historial.Alcohol = updateHistorialClinicoDto.Alcohol ?? historial.Alcohol;
            historial.Coffee = updateHistorialClinicoDto.Coffee ?? historial.Coffee;
            historial.Allergic = updateHistorialClinicoDto.Allergic ?? historial.Allergic;
            historial.Allergies = updateHistorialClinicoDto.Allergies ?? historial.Allergies;
            historial.TakesMedication = updateHistorialClinicoDto.TakesMedication ?? historial.TakesMedication;
            historial.Medication = updateHistorialClinicoDto.Medication ?? historial.Medication;
            historial.MedicalHistory = updateHistorialClinicoDto.MedicalHistory ?? historial.MedicalHistory;

            _context.HistorialClinicos.Update(historial);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHistorialClinicoAsync(int id)
        {
            var historial = await _context.HistorialClinicos.FindAsync(id);
            if (historial == null) throw new KeyNotFoundException("Historial clínico no encontrado");

            _context.HistorialClinicos.Remove(historial);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
