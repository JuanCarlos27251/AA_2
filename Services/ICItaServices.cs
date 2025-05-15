using AA2.Models;

namespace AA2.Services
{
    public interface ICitaServices
    {
        Task<List<Cita>> GetAllAsync();
        Task<Cita?> GetByIdAsync(int id);
        Task AddAsync(Cita cita);
        Task UpdateAsync(Cita cita);
        Task<bool> DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}