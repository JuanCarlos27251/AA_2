using Microsoft.AspNetCore.Mvc;
using AA2.Services;
using AA2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        // Endpoints públicos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CitaDtoOut>>> GetAll()
        {
            var citas = await _citaServices.GetAllAsync();

            // Si el usuario está autenticado, filtrar solo sus citas
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    citas = citas.Where(c => c.IdUsuario == int.Parse(userId)).ToList();
                }
            }

            return Ok(citas);
        }

        // Endpoints privados (usuarios autenticados)
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CitaDtoOut>> Add([FromBody] CitaDtoIn citaDtoIn)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("Usuario no autenticado");
                }

                // Asignar el ID del usuario actual a la cita
                citaDtoIn.IdUsuario = int.Parse(userId);

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
        [Authorize]
        public async Task<ActionResult> Update(int id, [FromBody] CitaDtoIn citaDtoIn)
        {
            // Verificar que la cita pertenece al usuario
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try 
            {
                // Obtener el ID del usuario actual
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("Usuario no autenticado");
                }

                // Primero obtener la cita para verificar que pertenece al usuario
                var cita = await _citaServices.GetByIdAsync(id);
                if (cita == null)
                {
                    return NotFound($"No se encontró la cita con ID: {id}");
                }

                // Verificar si el usuario es el propietario de la cita o es admin
                if (!User.IsInRole("Admin") && cita.IdUsuario.ToString() != userId)
                {
                    return StatusCode(403, "No tienes permiso para borrar esta cita");
                }

                var result = await _citaServices.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}