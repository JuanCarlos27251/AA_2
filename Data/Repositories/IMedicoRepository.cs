using AA2.Models;

namespace AA2.Data
{
    public interface IMedicoRepository
    {
        Task<List<Medico>> GetAllAsync();
        Task<Medico?> GetByIdAsync(int id);
        Task AddAsync(Medico medico);
        Task UpdateAsync(Medico medico);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}