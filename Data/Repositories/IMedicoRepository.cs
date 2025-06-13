using AA2.Models;

namespace AA2.Data
{
    public interface IMedicoRepository
    {
        Task<List<MedicoDtoOut>> GetAllAsync();
        Task<MedicoDtoOut?> GetByIdAsync(int id);
        Task<List<MedicoDtoOut>> SearchAsync(
            string? nombre, 
            string? especialidad, 
            string orderBy, 
            bool ascending);
        IQueryable<Medico> GetQueryable();
        
        Task<MedicoDtoOut> AddAsync(MedicoDtoIn medicoDto);
        Task UpdateAsync(int id, MedicoDtoIn medicoDto);
        Task<bool> DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}