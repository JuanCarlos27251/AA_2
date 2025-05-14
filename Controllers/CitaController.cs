using AA2.Models;
using AA2.Services;
using Microsoft.AspNetCore.Mvc;

namespace AA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CitaController : ControllerBase
    {
        private static List<Cita> citas = new List<Cita>();
        [HttpGet]
        public ActionResult<IEnumerable<Cita>> Get()
        {
            return Ok(citas);
        }

        [HttpGet("{id}")]
        public ActionResult<Cita> Get(int id)
        {
            var cita = citas.FirstOrDefault(u => u.Id == id);
            if (cita == null)
            {
                return NotFound();
            }
            return Ok(cita);
        }

        [HttpPost]
        public ActionResult<Cita> Post([FromBody] Cita cita)
        {
            if (cita == null)
            {
                return BadRequest();
            }
            cita.Id = citas.Count + 1;
            cita.IdMedico =citas.Count +1;
            cita.IdUsuario = citas.Count + 1;
            citas.Add(cita);
            return CreatedAtAction(nameof(Get), new { id = cita.Id }, cita);
        }

        [HttpPut("{id}")]
        public ActionResult<Cita> Put(int id, [FromBody] Cita cita)
        {
            if (cita == null || id != cita.Id)
            {
                return BadRequest();
            }
            var existingCita = citas.FirstOrDefault(u => u.Id == id);
            if (existingCita == null)
            {
                return NotFound();
            }
            existingCita.Motivo = cita.Motivo;
            existingCita.Confirmada = cita.Confirmada;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var cita = citas.FirstOrDefault(u => u.Id == id);
            if (cita == null)
            {
                return NotFound();
            }
            citas.Remove(cita);
            return NoContent();
        }
    }
}
    