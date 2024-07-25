using GestionMedicoBackend.DTOs.Consultorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultoriosController : ControllerBase
    {
        private readonly ConsultorioService _consultorioService;

        public ConsultoriosController(ConsultorioService consultorioService)
        {
            _consultorioService = consultorioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultorioDto>>> GetConsultorios()
        {
            var consultorios = await _consultorioService.GetAllConsultoriosAsync();
            return Ok(consultorios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultorioDto>> GetConsultorio(int id)
        {
            try
            {
                var consultorio = await _consultorioService.GetConsultorioByIdAsync(id);
                return Ok(consultorio);
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
        public async Task<ActionResult<ConsultorioDto>> CreateConsultorio(CreateConsultorioDto createConsultorioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var consultorio = await _consultorioService.CreateConsultorioAsync(createConsultorioDto);
                return CreatedAtAction(nameof(GetConsultorio), new { id = consultorio.Id }, consultorio);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsultorio(int id, UpdateConsultorioDto updateConsultorioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _consultorioService.UpdateConsultorioAsync(id, updateConsultorioDto);
                if (!result) return NotFound(new { message = "Consultorio no encontrado" });

                return Ok(new { message = "Consultorio actualizado correctamente." });
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
        public async Task<IActionResult> DeleteConsultorio(int id)
        {
            try
            {
                var result = await _consultorioService.DeleteConsultorioAsync(id);
                if (!result) return NotFound();

                return Ok(new { message = "Consultorio eliminado correctamente." });
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

        [HttpPatch("{id}/change-status")]
        public async Task<IActionResult> ToggleConsultorioStatus(int id)
        {
            try
            {
                var result = await _consultorioService.ToggleConsultorioStatusAsync(id);
                if (!result) return NotFound();

                return Ok(new { message = "Status del consultorio actualizado correctamente." });
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
        public async Task<IActionResult> ToggleConsultorioAvailability(int id)
        {
            try
            {
                var result = await _consultorioService.ToggleConsultorioAvailabilityAsync(id);
                if (!result) return NotFound();

                return Ok(new { message = "Disponibilidad del consultorio actualizada correctamente." });
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
