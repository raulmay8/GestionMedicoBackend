using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.Patient;
using GestionMedicoBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services.Patient
{
    public class PatientServices
    {
        private readonly ApplicationDbContext _context;

        public PatientServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    Occupation = patient.Occupation,
                    Picture = patient.Picture,
                    Phone = patient.Phone,
                    BloodGroup = patient.BloodGroup,
                    MaritalStatus = patient.MaritalStatus,
                    Address = patient.Address,
                    Gender = patient.Gender,
                    UserId = patient.UserId,
                    UserName = patient.User.Username
                })
                .ToListAsync();
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) throw new KeyNotFoundException("Paciente no encontrado");

            return new PatientDto
            {
                Id = patient.Id,
                Occupation = patient.Occupation,
                Picture = patient.Picture,
                Phone = patient.Phone,
                BloodGroup = patient.BloodGroup,
                MaritalStatus = patient.MaritalStatus,
                Address = patient.Address,
                Gender = patient.Gender,
                UserId = patient.UserId,
                UserName = patient.User.Username
            };
        }

        public async Task<string> CreatePatientAsync(CreatePatientDto createPatientDto)
        {
            var user = await _context.Users.FindAsync(createPatientDto.UserId);
            if (user == null) return "Error: Usuario no encontrado";

            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == createPatientDto.UserId);
            if (existingPatient != null) return "Error: El usuario ya está registrado como paciente.";

            var patient = new Models.Patient
            {
                Occupation = createPatientDto.Occupation,
                Picture = createPatientDto.Picture,
                Phone = createPatientDto.Phone,
                BloodGroup = createPatientDto.BloodGroup,
                MaritalStatus = createPatientDto.MaritalStatus,
                Address = createPatientDto.Address,
                Gender = createPatientDto.Gender,
                UserId = createPatientDto.UserId
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return "Paciente creado exitosamente";
        }

        public async Task<bool> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) throw new KeyNotFoundException("Paciente no encontrado");

            if (updatePatientDto.UserId != 0 && updatePatientDto.UserId != patient.UserId)
            {
                var user = await _context.Users.FindAsync(updatePatientDto.UserId);
                if (user == null) throw new KeyNotFoundException("Usuario no encontrado");
                patient.UserId = updatePatientDto.UserId;
            }

            patient.Occupation = updatePatientDto.Occupation ?? patient.Occupation;
            patient.Picture = updatePatientDto.Picture ?? patient.Picture;
            patient.Phone = updatePatientDto.Phone ?? patient.Phone;
            patient.BloodGroup = updatePatientDto.BloodGroup ?? patient.BloodGroup;
            patient.MaritalStatus = updatePatientDto.MaritalStatus ?? patient.MaritalStatus;
            patient.Address = updatePatientDto.Address ?? patient.Address;
            patient.Gender = updatePatientDto.Gender ?? patient.Gender;

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) throw new KeyNotFoundException("Paciente no encontrado");

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
