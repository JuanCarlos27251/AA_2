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

    }
}