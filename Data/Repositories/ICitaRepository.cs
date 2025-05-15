using AA2.Models;

namespace AA2.Data
{
    public interface ICitaRepository
    {
        Task<List<Cita>> GetAllAsync();
        Task<Cita?> GetByIdAsync(int id);
        Task AddAsync(Cita cita);
        Task UpdateAsync(Cita cita);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}