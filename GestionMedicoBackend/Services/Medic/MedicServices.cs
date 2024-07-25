using GestionMedicoBackend.Data;
using GestionMedicoBackend.DTOs.Medic;
using GestionMedicoBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Services.Medic
{
    public class MedicServices
    {
        private readonly ApplicationDbContext _context;

        public MedicServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicDto>> GetAllMedicsAsync()
        {
            return await _context.Medics
                .Include(m => m.User)
                .Include(m => m.Consultorio)
                .Select(medic => new MedicDto
                {
                    Id = medic.Id,
                    ProfessionalId = medic.ProfessionalId,
                    School = medic.School,
                    YearExperience = medic.YearExperience,
                    DateGraduate = medic.DateGraduate,
                    Availability = medic.Availability,
                    UserId = medic.UserId,
                    UserName = medic.User.Username,
                    ConsultorioId = medic.Consultorio.Id,
                    ConsultorioName = medic.Consultorio.Name
                })
                .ToListAsync();
        }

        public async Task<MedicDto> GetMedicByIdAsync(int id)
        {
            var medic = await _context.Medics
                .Include(m => m.User)
                .Include(m => m.Consultorio)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medic == null) throw new KeyNotFoundException("Médico no encontrado");

            return new MedicDto
            {
                Id = medic.Id,
                ProfessionalId = medic.ProfessionalId,
                School = medic.School,
                YearExperience = medic.YearExperience,
                DateGraduate = medic.DateGraduate,
                Availability = medic.Availability,
                UserId = medic.UserId,
                UserName = medic.User.Username,
                ConsultorioId = medic.Consultorio.Id,
                ConsultorioName = medic.Consultorio.Name
            };
        }

        public async Task<MedicDto> CreateMedicAsync(CreateMedicDto createMedicDto)
        {
            var user = await _context.Users.FindAsync(createMedicDto.UserId);
            if (user == null) throw new KeyNotFoundException("Usuario no encontrado");

            var consultorio = await _context.Consultorios.FindAsync(createMedicDto.ConsultorioId);
            if (consultorio == null) throw new KeyNotFoundException("Consultorio no encontrado");

            var existingMedic = await _context.Medics.FirstOrDefaultAsync(m => m.UserId == createMedicDto.UserId);
            if (existingMedic != null) throw new InvalidOperationException("El usuario ya está registrado como médico.");

            try
            {
                var dateGraduate = new DateOnly(createMedicDto.Year, createMedicDto.Month, createMedicDto.Day);

                var medic = new Models.Medic
                {
                    ProfessionalId = createMedicDto.ProfessionalId,
                    School = createMedicDto.School,
                    YearExperience = createMedicDto.YearExperience,
                    DateGraduate = dateGraduate,
                    Availability = true,
                    UserId = createMedicDto.UserId,
                    ConsultorioId = createMedicDto.ConsultorioId
                };

                _context.Medics.Add(medic);
                await _context.SaveChangesAsync();

                return new MedicDto
                {
                    Id = medic.Id,
                    ProfessionalId = medic.ProfessionalId,
                    School = medic.School,
                    YearExperience = medic.YearExperience,
                    DateGraduate = medic.DateGraduate,
                    Availability = medic.Availability,
                    UserId = medic.UserId,
                    UserName = user.Username,
                    ConsultorioId = consultorio.Id,
                    ConsultorioName = consultorio.Name
                };
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentException("Fecha de graduación inválida");
            }
        }

        public async Task<bool> UpdateMedicAsync(int id, UpdateMedicDto updateMedicDto)
        {
            var medic = await _context.Medics.FindAsync(id);
            if (medic == null) throw new KeyNotFoundException("Médico no encontrado");

            if (updateMedicDto.UserId != 0 && updateMedicDto.UserId != medic.UserId)
            {
                var user = await _context.Users.FindAsync(updateMedicDto.UserId);
                if (user == null) throw new KeyNotFoundException("Usuario no encontrado");
                medic.UserId = updateMedicDto.UserId;
            }

            if (updateMedicDto.ConsultorioId != 0 && updateMedicDto.ConsultorioId != medic.ConsultorioId)
            {
                var consultorio = await _context.Consultorios.FindAsync(updateMedicDto.ConsultorioId);
                if (consultorio == null) throw new KeyNotFoundException("Consultorio no encontrado");
                medic.ConsultorioId = updateMedicDto.ConsultorioId;
            }

            medic.ProfessionalId = updateMedicDto.ProfessionalId ?? medic.ProfessionalId;
            medic.School = updateMedicDto.School ?? medic.School;
            medic.YearExperience = updateMedicDto.YearExperience != 0 ? updateMedicDto.YearExperience : medic.YearExperience;
            if (updateMedicDto.Year != 0 && updateMedicDto.Month != 0 && updateMedicDto.Day != 0)
            {
                medic.DateGraduate = new DateOnly(updateMedicDto.Year, updateMedicDto.Month, updateMedicDto.Day);
            }

            _context.Medics.Update(medic);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMedicAsync(int id)
        {
            var medic = await _context.Medics.FindAsync(id);
            if (medic == null) throw new KeyNotFoundException("Médico no encontrado");

            _context.Medics.Remove(medic);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleMedicAvailabilityAsync(int id)
        {
            var medic = await _context.Medics.FindAsync(id);
            if (medic == null) throw new KeyNotFoundException("Médico no encontrado");

            medic.Availability = !medic.Availability;

            _context.Medics.Update(medic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
