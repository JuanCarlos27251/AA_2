using AA2.Models;

namespace AA2.Services
{
    public interface ICitaServices
    {
        Task<List<CitaDtoOut>> GetAllAsync();
        Task<CitaDtoOut?> GetByIdAsync(int id);
        Task<CitaDtoOut> AddAsync(CitaDtoIn citaDtoIn);
        Task UpdateAsync(int id, CitaDtoIn citaDtoIn);
        Task<bool> DeleteAsync(int id);
        Task InicializarDatosAsync();
        
        // Task<List<Cita>> GetAllAsync();
        // Task<Cita?> GetByIdAsync(int id);
        // Task AddAsync(Cita cita);
        // Task UpdateAsync(Cita cita);
        // Task<bool> DeleteAsync(int id);
        // Task InicializarDatosAsync();
    }
}