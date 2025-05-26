using Microsoft.AspNetCore.Mvc;
using AA2.Services;
using AA2.Models;
using Microsoft.AspNetCore.Authorization;

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
        public ActionResult<IEnumerable<UsuarioDtoOut>> GetAll()
        {
            var usuarios = _usuarioServices.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")] // Solo admin puede buscar usuarios
        public async Task<ActionResult<IEnumerable<UsuarioDtoOut>>> Search(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] string orderBy = "fecha",
            [FromQuery] bool ascending = true)
        {
            try
            {
                var usuarios = await _usuarioServices.SearchAsync(
                    fechaInicio,
                    fechaFin,
                    orderBy,
                    ascending);

                return Ok(usuarios);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Solo admin puede crear usuarios
        public ActionResult<UsuarioDtoOut> Add([FromBody]UsuarioDtoin usuario)
        {
            _usuarioServices.Add(usuario);
            return StatusCode(201, usuario);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Solo admin puede actualizar usuarios
        public IActionResult Update(int id, [FromBody] UsuarioDtoin usuario)
        {
            try
            {
                _usuarioServices.Update(id, usuario); 
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Solo admin puede eliminar usuarios
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioServices.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}