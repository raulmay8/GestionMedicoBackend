using Microsoft.AspNetCore.Mvc;
using GestionMedicoBackend.DTOs.Medico;
using GestionMedicoBackend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly MedicoService _medicoService;

        public MedicoController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        // GET: api/Medico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicoDto>>> GetMedicos()
        {
            var medicos = await _medicoService.GetAllMedicosAsync();
            return Ok(medicos);
        }

        // GET: api/Medico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDto>> GetMedico(int id)
        {
            var medico = await _medicoService.GetMedicoByIdAsync(id);

            if (medico == null)
            {
                return NotFound();
            }

            return Ok(medico);
        }

        // POST: api/Medico
        [HttpPost]
        public async Task<ActionResult<MedicoDto>> CreateMedico(CreateMedicoDto createMedicoDto)
        {
            var medico = await _medicoService.CreateMedicoAsync(createMedicoDto);
            return CreatedAtAction(nameof(GetMedico), new { id = medico.PkMedico }, medico);
        }

        // PUT: api/Medico/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedico(int id, UpdateMedicoDto updateMedicoDto)
        {
            var result = await _medicoService.UpdateMedicoAsync(id, updateMedicoDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Medico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var result = await _medicoService.DeleteMedicoAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
