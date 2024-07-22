using GestionMedicoBackend.DTOs.Specialty;
using GestionMedicoBackend.Services.Specialty;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers.Specialty
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly SpecialtyServices _specialtyService;

        public SpecialtiesController(SpecialtyServices specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialtyDto>>> GetSpecialties()
        {
            var specialties = await _specialtyService.GetAllSpecialtiesAsync();
            return Ok(specialties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialtyDto>> GetSpecialty(int id)
        {
            try
            {
                var specialty = await _specialtyService.GetSpecialtyByIdAsync(id);
                return Ok(specialty);
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
        public async Task<IActionResult> CreateSpecialty(CreateSpecialtyDto createSpecialtyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _specialtyService.CreateSpecialtyAsync(createSpecialtyDto);

            if (result.StartsWith("Error"))
            {
                return BadRequest(new { message = result });
            }

            return Ok(new { message = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpecialty(int id, UpdateSpecialtyDto updateSpecialtyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _specialtyService.UpdateSpecialtyAsync(id, updateSpecialtyDto);

            if (result.StartsWith("Error"))
            {
                return NotFound(new { message = result });
            }

            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            var result = await _specialtyService.DeleteSpecialtyAsync(id);

            if (result.StartsWith("Error"))
            {
                return NotFound(new { message = result });
            }

            return Ok(new { message = result });
        }
    }
}
