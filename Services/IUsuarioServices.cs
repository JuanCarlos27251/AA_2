using AA2.Models;

namespace AA2.Services
{
    public interface IUsuarioServices
    {
        IEnumerable<UsuarioDtoOut> GetAll();
        UsuarioDtoOut Get(int id);
        void Add(UsuarioDtoin usuario);
        
        void Update(int id, UsuarioDtoin usuario);
        Task<bool> DeleteAsync(int id);
        Task InicializarDatosAsync();
        Task<UsuarioDtoOut> GetUserFromCredentialsAsync(LoginDtoIn loginDtoIn);
        Task<UsuarioDtoOut> AddUserFromCredentialsAsync(UsuarioDtoin usuarioDtoin);
    }
}
