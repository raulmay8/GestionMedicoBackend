using GestionMedicoBackend.DTOs.Horario;
using GestionMedicoBackend.Services.Horario;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers.Horario
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly HorarioServices _horarioService;

        public HorarioController(HorarioServices horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDto>>> GetHorarios()
        {
            var horarios = await _horarioService.GetAllHorariosAsync();
            return Ok(horarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDto>> GetHorario(int id)
        {
            try
            {
                var horario = await _horarioService.GetHorarioByIdAsync(id);
                return Ok(horario);
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
        public async Task<IActionResult> CreateHorario(CreateHorarioDto createHorarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _horarioService.CreateHorarioAsync(createHorarioDto);

            if (result.StartsWith("Error"))
            {
                return BadRequest(new { message = result });
            }

            return Ok(new { message = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHorario(int id, UpdateHorarioDto updateHorarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _horarioService.UpdateHorarioAsync(id, updateHorarioDto);
                if (!result) return NotFound(new { message = "Horario no encontrado" });

                return Ok(new { message = "Horario actualizado correctamente." });
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
        public async Task<IActionResult> DeleteHorario(int id)
        {
            try
            {
                var result = await _horarioService.DeleteHorarioAsync(id);
                if (!result) return NotFound();

                return Ok(new { message = "Horario eliminado correctamente." });
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
