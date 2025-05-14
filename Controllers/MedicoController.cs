using AA2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MedicoController : ControllerBase
    {
        private static List<Medico> medicos = new List<Medico>();
        [HttpGet]
        public ActionResult<IEnumerable<Medico>> Get()
        {
            return Ok(medicos);
        }
        [HttpGet("{id}")]
        public ActionResult<Medico> Get(int id)
        {
            var medico = medicos.FirstOrDefault(u => u.Id == id);
            if (medico == null)
            {
                return NotFound();
            }
            return Ok(medico);
        }

        [HttpPost]
        public ActionResult<Medico> Post([FromBody] Medico medico)
        {
            if (medico == null)
            {
                return BadRequest();
            }
            medico.Id = medicos.Count + 1;
            medicos.Add(medico);
            return CreatedAtAction(nameof(Get), new { id = medico.Id }, medico);
        }

        [HttpPut("{id}")]
        public ActionResult<Medico> Put(int id, [FromBody] Medico medico)
        {
            if (medico == null || id != medico.Id)
            {
                return BadRequest();
            }
            var existingMedico = medicos.FirstOrDefault(u => u.Id == id);
            if (existingMedico == null)
            {
                return NotFound();
            }
            existingMedico.Nombre = medico.Nombre;
            existingMedico.Especialidad = medico.Especialidad;
            existingMedico.Email = medico.Email;
            existingMedico.Telefono = medico.Telefono;
            existingMedico.Disponible = medico.Disponible;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var medico = medicos.FirstOrDefault(u => u.Id == id);
            if (medico == null)
            {
                return NotFound();
            }
            medicos.Remove(medico);
            return NoContent();
        }
    }
}
    