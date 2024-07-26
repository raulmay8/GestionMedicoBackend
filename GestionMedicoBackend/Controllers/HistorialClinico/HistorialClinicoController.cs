using GestionMedicoBackend.DTOs.HistorialClinico;
using GestionMedicoBackend.Services.HistorialClinico;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers.HistorialClinico
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialClinicoController : ControllerBase
    {
        private readonly HistorialClinicoServices _historialClinicoService;

        public HistorialClinicoController(HistorialClinicoServices historialClinicoService)
        {
            _historialClinicoService = historialClinicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialClinicoDto>>> GetHistorialClinico()
        {
            var historiales = await _historialClinicoService.GetAllHistorialClinicoAsync();
            return Ok(historiales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialClinicoDto>> GetHistorialClinico(int id)
        {
            try
            {
                var historial = await _historialClinicoService.GetHistorialClinicoByIdAsync(id);
                return Ok(historial);
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
        public async Task<IActionResult> CreateHistorialClinico(CreateHistorialClinicoDto createHistorialClinicoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _historialClinicoService.CreateHistorialClinicoAsync(createHistorialClinicoDto);

            if (result.StartsWith("Error"))
            {
                return BadRequest(new { message = result });
            }

            return Ok(new { message = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistorialClinico(int id, UpdateHistorialClinicoDto updateHistorialClinicoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _historialClinicoService.UpdateHistorialClinicoAsync(id, updateHistorialClinicoDto);
                if (!result) return NotFound(new { message = "Historial clínico no encontrado" });

                return Ok(new { message = "Historial clínico actualizado correctamente." });
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
        public async Task<IActionResult> DeleteHistorialClinico(int id)
        {
            try
            {
                var result = await _historialClinicoService.DeleteHistorialClinicoAsync(id);
                if (!result) return NotFound();

                return Ok(new { message = "Historial clínico eliminado correctamente." });
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
