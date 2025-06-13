using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Runtime.Serialization;
using AA2.Models;
using AA2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace AA2.Services
{

    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _repository;

        public AuthServices(IConfiguration configuration, IUsuarioRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public string Login(LoginDtoIn loginDtoIn)
        {
            var usuario = _repository.GetUserFromCredentials(loginDtoIn);
            return GenerateToken(usuario);
        }

        public string Register(UsuarioDtoin usuarioDtoin)
        {
            try
            {
                // Verificar si el usuario ya existe
                var existingUser = _repository.GetUserByEmail(usuarioDtoin.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("El email ya está registrado");
                }

                // Añadir el nuevo usuario y obtener el DTO de salida
                var usuarioDtoOut = _repository.AddUserFromCredentials(usuarioDtoin);

                // Generar y devolver el token
                return GenerateToken(usuarioDtoOut);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el registro: {ex.Message}");
            }
        }

        public string GenerateToken(UsuarioDtoOut usuarioDtoOut)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescrptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:ValidIssuer"],
                Audience = _configuration["Jwt:ValidAudience"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(usuarioDtoOut.Id)),
                    new Claim(ClaimTypes.Name, usuarioDtoOut.Nombre),
                    new Claim(ClaimTypes.Email, usuarioDtoOut.Email),
                    new Claim(ClaimTypes.Role, usuarioDtoOut.Rol),
                    new Claim("FechaRegistro", usuarioDtoOut.FechaRegistro.ToString()),
                    new Claim("myCustomClaim", "myCustomClaimValue")
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokendHandler = new JwtSecurityTokenHandler();
            var token = tokendHandler.CreateToken(tokenDescrptor);
            var tokenString = tokendHandler.WriteToken(token);
            return tokenString;
        }

        public bool HasAccessToResource(int requestUsuarioId, ClaimsPrincipal usuario)
        {
            var usuarioIdClaim = usuario.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (usuarioIdClaim is null || !int.TryParse(usuarioIdClaim.Value, out int usuarioId))
            {
                return false;
            }
            var isOwnResource = usuarioId == requestUsuarioId;
            var roleClaim = usuario.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim != null) return false;
            var isAdmin = roleClaim!.Value == Roles.Admin;

            var hasAccess = isOwnResource || isAdmin;
            return hasAccess;

        }
        
    }
}