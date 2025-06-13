using AA2.Models;

namespace AA2.Data
{
    public interface IUsuarioRepository
    {

        public IEnumerable<UsuarioDtoOut> GetAll();

        public void Add(UsuarioDtoin usuario);

        void Update(int id, UsuarioDtoin usuarioDtoin);
        Task<bool> DeleteAsync(int id);
        Task InicializarDatosAsync();
        public UsuarioDtoOut GetUserFromCredentials(LoginDtoIn loginDtoIn);
        public UsuarioDtoOut AddUserFromCredentials(UsuarioDtoin usuarioDtoin);
        UsuarioDtoOut? GetUserByEmail(string email);
        Task<List<UsuarioDtoOut>> SearchAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string orderBy,
            bool ascending);
        IQueryable<Usuario> GetQueryable();
    }
}
