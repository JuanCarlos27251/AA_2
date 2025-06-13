using AA2.Models;

namespace AA2.Data
{
    public interface ICitaRepository
    {
        Task<List<CitaDtoOut>> GetAllAsync();
        Task<CitaDtoOut?> GetByIdAsync(int id);
        Task<CitaDtoOut> AddAsync(CitaDtoIn citaDtoIn);
        Task UpdateAsync(int id, CitaDtoIn citaDtoIn);
        Task<bool> DeleteAsync(int id);
        Task InicializarDatosAsync();
        Task<List<CitaDtoOut>> SearchAsync(
            int userId,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string? nombreMedico,
            string orderBy,
            bool ascending);
        IQueryable<Cita> GetQueryable();
    }
}