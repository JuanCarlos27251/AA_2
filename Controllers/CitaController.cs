using Microsoft.AspNetCore.Mvc;
using AA2.Services;
using AA2.Models;


namespace AA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CitaController : ControllerBase
    {
        private readonly ICitaServices _citaServices;

        public CitaController(ICitaServices citaServices)
        {
            _citaServices = citaServices;
        }

                [HttpGet]
        public async Task<ActionResult<List<CitaDtoOut>>> GetAll()
        {
            var citas = await _citaServices.GetAllAsync();
            return Ok(citas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDtoOut>> GetById(int id)
        {
            var cita = await _citaServices.GetByIdAsync(id);
            if (cita == null)
            {
                return NotFound($"No se encontró la cita con ID: {id}");
            }
            return Ok(cita);
        }

        [HttpPost]
        public async Task<ActionResult<CitaDtoOut>> Add([FromBody] CitaDtoIn citaDtoIn)
        {
            try
            {
                var cita = await _citaServices.AddAsync(citaDtoIn);
                return CreatedAtAction(nameof(GetById), new { id = cita.Id }, cita);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CitaDtoIn citaDtoIn)
        {
            try
            {
                await _citaServices.UpdateAsync(id, citaDtoIn);
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
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _citaServices.DeleteAsync(id);
            if (!result)
            {
                return NotFound($"No se encontró la cita con ID: {id}");
            }
            return NoContent();
        }



        // [HttpGet]
        // public async Task<ActionResult<List<Cita>>> GetAll()
        // {
        //     var citas = await _citaServices.GetAllAsync();
        //     return Ok(citas);
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Cita>> GetById(int id)
        // {
        //     var cita = await _citaServices.GetByIdAsync(id);
        //     if (cita == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(cita);
        // }

        // [HttpPost]
        // public async Task<ActionResult> Add(Cita cita)
        // {
        //     await _citaServices.AddAsync(cita);
        //     return CreatedAtAction(nameof(GetById), new { id = cita.Id }, cita);
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult> Update(int id, Cita cita)
        // {
        //     if (id != cita.Id)
        //     {
        //         return BadRequest();
        //     }
        //     await _citaServices.UpdateAsync(cita);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var result = await _citaServices.DeleteAsync(id);
        //     if (!result)
        //     {
        //         return NotFound();
        //     }
        //     return NoContent();
        // }
    }
}