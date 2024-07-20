using GestionMedicoBackend.DTOs.Especialidad;
using GestionMedicoBackend.Services.Especialidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionMedicoBackend.Controllers.Especialidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly EspecialidadService _especialidadService;

        public EspecialidadesController(EspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadDto>>> GetEspecialidades()
        {
            var especialidades = await _especialidadService.GetAllEspecialidadesAsync();
            return Ok(especialidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadDto>> GetEspecialidad(int id)
        {
            try
            {
                var especialidad = await _especialidadService.GetEspecialidadByIdAsync(id);
                return Ok(especialidad);
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
        /*[HttpPost]
        public async Task<ActionResult<EspecialidadDto>> CreateEspecialidad(CreateEspecialidadDto createEspecialidadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var especialidad = await _especialidadService.CreateEspecialidadAsync(createEspecialidadDto);
                return CreatedAtAction(nameof(GetEspecialidad), new
                {
*/