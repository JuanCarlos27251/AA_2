using Microsoft.EntityFrameworkCore;
using AA2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AA2.Data
{
    public class UsuarioEfRepository : IUsuarioRepository
    {
        private readonly AA2DbContext _dbcontext;

        public UsuarioEfRepository(AA2DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Add(UsuarioDtoin usuarioDtoin)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDtoin.Nombre,
                Email = usuarioDtoin.Email,
                Contrasena = usuarioDtoin.Contrasena,
                FechaRegistro = DateTime.Now,
                Rol = "Paciente", // Por defecto, los nuevos usuarios son pacientes
                EstaActivo = true
            };

            _dbcontext.Usuarios.Add(usuario);
            _dbcontext.SaveChanges();
        }
        public IEnumerable<UsuarioDtoOut> GetAll()
        {
            return _dbcontext.Usuarios.Select(u => new UsuarioDtoOut
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                FechaRegistro = u.FechaRegistro,
                Rol = u.Rol // Asigna directamente el valor del enum Roles
            }).ToList();
        }
        
        public UsuarioDtoOut Get(int id)
        {
            var usuario = _dbcontext.Usuarios.FirstOrDefault(u => u.Id == id);
            
            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado");
            }

            return new UsuarioDtoOut 
            { 
                Id = usuario.Id, 
                Nombre = usuario.Nombre, 
                Email = usuario.Email,
                FechaRegistro = usuario.FechaRegistro,
                Rol = usuario.Rol
            };
        }
        
        public void Update(int id, UsuarioDtoin usuarioDtoin)
        {
            var usuario = _dbcontext.Usuarios.FirstOrDefault(u => u.Id == id);
            
            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado");
            }

            // Actualizamos los campos modificables
            usuario.Nombre = usuarioDtoin.Nombre;
            usuario.Email = usuarioDtoin.Email;
            usuario.Contrasena = usuarioDtoin.Contrasena;
            usuario.EstaActivo = usuarioDtoin.EstaActivo;
            
            _dbcontext.Usuarios.Update(usuario);
            _dbcontext.SaveChanges();
        }

        public UsuarioDtoOut AddUserFromCredentials(UsuarioDtoin usuarioDtoin)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDtoin.Nombre,
                Email = usuarioDtoin.Email,
                Contrasena = usuarioDtoin.Contrasena,
                FechaRegistro = DateTime.Now,
                Rol = "Paciente", // Por defecto
                EstaActivo = true
            };

            _dbcontext.Usuarios.Add(usuario);
            _dbcontext.SaveChanges();

            return new UsuarioDtoOut
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol,
                FechaRegistro = usuario.FechaRegistro
            };
        }

        public UsuarioDtoOut GetUserFromCredentials(LoginDtoIn loginDtoIn)
        {
            // Buscar el usuario en la base de datos
            var usuario = _dbcontext.Usuarios
                .FirstOrDefault(u => u.Email == loginDtoIn.Email && u.Contrasena == loginDtoIn.Contrasena);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            return new UsuarioDtoOut 
            { 
                Id = usuario.Id, 
                Nombre = usuario.Nombre, 
                Email = usuario.Email, 
                FechaRegistro = usuario.FechaRegistro,
                Rol = usuario.Rol 
            };
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

        public UsuarioDtoOut? GetUserByEmail(string email)
        {
            var usuario = _dbcontext.Usuarios.FirstOrDefault(u => u.Email == email);
            
            if (usuario == null)
                return null;

            return new UsuarioDtoOut
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol,
                FechaRegistro = usuario.FechaRegistro
            };
        }
    }
}