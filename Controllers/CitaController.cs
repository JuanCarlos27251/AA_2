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
        public async Task<ActionResult<List<Cita>>> GetAll()
        {
            var citas = await _citaServices.GetAllAsync();
            return Ok(citas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetById(int id)
        {
            var cita = await _citaServices.GetByIdAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            return Ok(cita);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Cita cita)
        {
            await _citaServices.AddAsync(cita);
            return CreatedAtAction(nameof(GetById), new { id = cita.Id }, cita);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Cita cita)
        {
            if (id != cita.Id)
            {
                return BadRequest();
            }
            await _citaServices.UpdateAsync(cita);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _citaServices.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}