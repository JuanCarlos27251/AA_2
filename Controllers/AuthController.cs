using Microsoft.AspNetCore.Mvc;
using AA2.Models;
using AA2.Services;

namespace AA2.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDtoIn loginDtoIn)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                var token = _authServices.Login(loginDtoIn);
                return Ok(token);

            }
            catch (KeyNotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest
                ("Error generating token: " + ex.Message);
            }
        }

        [HttpPost("Register")]
        public IActionResult Register(UsuarioDtoin usuarioDtoin)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                var token = _authServices.Register(usuarioDtoin);
                return Ok(token);

            }
            catch (Exception ex)
            {
                return BadRequest
                ("Error generating token: " + ex.Message);
            }
        }
        

    }
}