using Microsoft.AspNetCore.Mvc;
using AA2.Services;
using AA2.Models;

namespace AA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private static List<Usuario> _usuarios = new List<Usuario>();
        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuario()
        {
            var usuarios = await _usuarioServices.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioServices.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
                
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario( Usuario usuario)
        {
            await _usuarioServices.AddAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            await _usuarioServices.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioServices.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}