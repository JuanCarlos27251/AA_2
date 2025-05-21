using Microsoft.EntityFrameworkCore;
using AA2.Models;

namespace AA2.Data
{
    public class UsuarioEfRepository : IUsuarioRepository
    {
        private readonly AA2DbContext _dbcontext;

        public UsuarioEfRepository(AA2DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _dbcontext.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _dbcontext.Usuarios.FindAsync(id);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _dbcontext.Usuarios.AddAsync(usuario);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _dbcontext.Usuarios.Update(usuario);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _dbcontext.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _dbcontext.Usuarios.Remove(usuario);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task InicializarDatosAsync()
        {
            if (!await _dbcontext.Usuarios.AnyAsync())
            {
                var usuarios = new List<Usuario>
                {
                    new Usuario
                {
                    Id = 1,
                    Nombre = "admin",
                    Email = "admin@gmail.com",
                    Contrasena = "admin123",
                    FechaRegistro = DateTime.Parse("2025-05-13T15:12:36.2905962+02:00"),
                    Rol = "Admin",
                    EstaActivo = true
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Pepe",
                    Email = "pepe@gmail.com",
                    Contrasena = "123456",
                    FechaRegistro = DateTime.Parse("2025-05-13T15:12:36.2906353+02:00"),
                    Rol = "Paciente",
                    EstaActivo = true
                },
                new Usuario
                {
                    Id = 3,
                    Nombre = "Maria",
                    Email = "maria@gmail.com",
                    Contrasena = "123456",
                    FechaRegistro = DateTime.Parse("2025-05-13T15:12:36.2906357+02:00"),
                    Rol = "Paciente",
                    EstaActivo = true
                },
                new Usuario
                {
                    Id = 4,
                    Nombre = "juan",
                    Email = "juan@gmail.com",
                    Contrasena = "123456789",
                    FechaRegistro = DateTime.Parse("2025-05-13T15:19:07.3008026+02:00"),
                    Rol = "Paciente",
                    EstaActivo = true
                }
                };

                await _dbcontext.Usuarios.AddRangeAsync(usuarios);
                await _dbcontext.SaveChangesAsync();
            }
        }


    }
}