/*using Microsoft.AspNetCore.Mvc;
using GestionMedicoBackend.DTOs.Horario;
using GestionMedicoBackend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly HorarioService _horarioService;

        public HorarioController(HorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        // GET: api/Horario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDto>>> GetHorarios()
        {
            var horarios = await _horarioService.GetAllHorariosAsync();
            return Ok(horarios);
        }

        // GET: api/Horario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDto>> GetHorario(int id)
        {
            var horario = await _horarioService.GetHorarioByIdAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            return Ok(horario);
        }

        // POST: api/Horario
        [HttpPost]
        public async Task<ActionResult<HorarioDto>> CreateHorario(CreateHorarioDto createHorarioDto)
        {
            var horario = await _horarioService.CreateHorarioAsync(createHorarioDto);
            return CreatedAtAction(nameof(GetHorario), new { id = horario.Id }, horario);
        }

        // PUT: api/Horario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHorario(int id, UpdateHorarioDto updateHorarioDto)
        {
            var result = await _horarioService.UpdateHorarioAsync(id, updateHorarioDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Horario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var result = await _horarioService.DeleteHorarioAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
*/