using GestionMedicoBackend.DTOs.Medic;
using GestionMedicoBackend.Services.Medic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers.Medic
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicsController : ControllerBase
    {
        private readonly MedicServices _medicService;

        public MedicsController(MedicServices medicService)
        {
            _medicService = medicService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicDto>>> GetMedics()
        {
            var medics = await _medicService.GetAllMedicsAsync();
            return Ok(medics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicDto>> GetMedic(int id)
        {
            try
            {
                var medic = await _medicService.GetMedicByIdAsync(id);
                return Ok(medic);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<MedicDto>> CreateMedic(CreateMedicDto createMedicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var medic = await _medicService.CreateMedicAsync(createMedicDto);
                return CreatedAtAction(nameof(GetMedic), new { id = medic.Id }, medic);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedic(int id, UpdateMedicDto updateMedicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _medicService.UpdateMedicAsync(id, updateMedicDto);
                if (!result) return NotFound(new { message = "Médico no encontrado" });

                return Ok(new { message = "Médico actualizado correctamente." });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedic(int id)
        {
            try
            {
                var result = await _medicService.DeleteMedicAsync(id);
                if (!result) return NotFound();

                return Ok(new { message = "Médico eliminado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/change-availability")]
        public async Task<IActionResult> ToggleMedicAvailability(int id)
        {
            try
            {
                var result = await _medicService.ToggleMedicAvailabilityAsync(id);
                if (!result) return NotFound();

                return Ok(new { message = "Disponibilidad del médico actualizada correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}