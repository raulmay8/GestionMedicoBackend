using GestionMedicoBackend.DTOs.Consultorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers.Consultorio
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultorioController : ControllerBase
    {
        private readonly ConsultorioService _consultorioService;

        public ConsultorioController(ConsultorioService consultorioService)
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
            var consultorio = await _consultorioService.GetConsultorioByIdAsync(id);
            if (consultorio == null) return NotFound();

            return Ok(consultorio);
        }

        [HttpPost]
        public async Task<ActionResult<ConsultorioDto>> CreateConsultorio(CreateConsultorioDto createConsultorioDto)
        {
            try
            {
                var consultorio = await _consultorioService.CreateConsultorioAsync(createConsultorioDto);
                return CreatedAtAction(nameof(GetConsultorio), new { id = consultorio.PkConsultorio }, consultorio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsultorio(int id, UpdateConsultorioDto updateConsultorioDto)
        {
            try
            {
                var result = await _consultorioService.UpdateConsultorioAsync(id, updateConsultorioDto);
                if (!result) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultorio(int id)
        {
            var result = await _consultorioService.DeleteConsultorioAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
