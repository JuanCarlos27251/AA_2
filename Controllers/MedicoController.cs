using Microsoft.AspNetCore.Mvc;
using AA2.Services;
using AA2.Models;


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

                [HttpGet]
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

        [HttpPost]
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
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _medicoServices.DeleteAsync(id);
            if (!result)
            {
                return NotFound($"No se encontró el médico con ID: {id}");
            }
            return NoContent();
        }

        // [HttpGet]
        // public async Task<ActionResult<List<Medico>>> GetAll()
        // {
        //     var medicos = await _medicoServices.GetAllAsync();
        //     return Ok(medicos);
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Medico>> GetById(int id)
        // {
        //     var medico = await _medicoServices.GetByIdAsync(id);
        //     if (medico == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(medico);
        // }

        // [HttpPost]
        // public async Task<ActionResult> Add(Medico medico)
        // {
        //     await _medicoServices.AddAsync(medico);
        //     return CreatedAtAction(nameof(GetById), new { id = medico.Id }, medico);
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult> Update(int id, Medico medico)
        // {
        //     if (id != medico.Id)
        //     {
        //         return BadRequest();
        //     }
        //     await _medicoServices.UpdateAsync(medico);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     var result = await _medicoServices.DeleteAsync(id);
        //     if (!result)
        //     {
        //         return NotFound();
        //     }
        //     return NoContent();
        // }

    }

}