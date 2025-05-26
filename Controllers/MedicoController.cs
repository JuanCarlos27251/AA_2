using Microsoft.AspNetCore.Mvc;
using AA2.Services;
using AA2.Models;
using Microsoft.AspNetCore.Authorization;


namespace AA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoServices _medicoServices;

        public MedicoController(IMedicoServices medicoServices)
        {
            _medicoServices = medicoServices;
        }

        // Endpoints públicos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<MedicoDtoOut>>> GetAll()
        {
            var medicos = await _medicoServices.GetAllAsync();
            return Ok(medicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDtoOut>> GetById(int id)
        {
            var medico = await _medicoServices.GetByIdAsync(id);
            if (medico == null)
            {
                return NotFound($"No se encontró el médico con ID: {id}");
            }
            return Ok(medico);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MedicoDtoOut>>> Search(
            [FromQuery] string? nombre,
            [FromQuery] string? especialidad,
            [FromQuery] string? orderBy = "nombre",
            [FromQuery] bool ascending = true)
        {
            try
            {
                var medicos = await _medicoServices.SearchAsync(nombre, especialidad, orderBy, ascending);
                return Ok(medicos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoints privados (solo admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MedicoDtoOut>> Add([FromBody] MedicoDtoIn medicoDto)
        {
            try
            {
                var medico = await _medicoServices.AddAsync(medicoDto);
                return CreatedAtAction(nameof(GetById), new { id = medico.Id }, medico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(int id, [FromBody] MedicoDtoIn medicoDto)
        {
            try
            {
                await _medicoServices.UpdateAsync(id, medicoDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _medicoServices.DeleteAsync(id);
            if (!result)
            {
                return NotFound($"No se encontró el médico con ID: {id}");
            }
            return NoContent();
        }

    }

}