using AA2.Models;

namespace AA2.Services
{
    public interface IUsuarioServices
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
    
}