using AA2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private static List<Usuario> usuarios = new List<Usuario>();
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public ActionResult<Usuario> Post([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }
            usuario.Id = usuarios.Count + 1;
            usuarios.Add(usuario);
            return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || id != usuario.Id)
            {
                return BadRequest();
            }
            var existingUsuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (existingUsuario == null)
            {
                return NotFound();
            }
            existingUsuario.Nombre = usuario.Nombre;
            existingUsuario.Email = usuario.Email;
            existingUsuario.EstaActivo = usuario.EstaActivo;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuarios.Remove(usuario);
            return NoContent();
        }
    }
}
    