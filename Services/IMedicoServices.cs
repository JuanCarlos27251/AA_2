using AA2.Models;

namespace AA2.Services
{
    public interface IMedicoServices
    {
        Task<List<MedicoDtoOut>> GetAllAsync();
        Task<MedicoDtoOut?> GetByIdAsync(int id);
        Task<MedicoDtoOut> AddAsync(MedicoDtoIn medicoDto);
        Task UpdateAsync(int id, MedicoDtoIn medicoDto);
        Task<bool> DeleteAsync(int id);
        Task InicializarDatosAsync();

        // Task<List<Medico>> GetAllAsync();
        // Task<Medico?> GetByIdAsync(int id);
        // Task AddAsync(Medico medico);
        // Task UpdateAsync(Medico medico);
        // Task<bool> DeleteAsync(int id);
        // Task InicializarDatosAsync();
    }
}