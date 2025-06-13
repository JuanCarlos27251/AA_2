using System.Security.Claims;
using AA2.Models;

namespace AA2.Services
{
    public interface IAuthServices
    {
        public string Login(LoginDtoIn usuarioDtoIn);
        public string Register(UsuarioDtoin usuarioDtoin);

        public string GenerateToken(UsuarioDtoOut usuarioDtoOut);
        public bool HasAccessToResource(int requestedUserId, ClaimsPrincipal user);
        
    }

}