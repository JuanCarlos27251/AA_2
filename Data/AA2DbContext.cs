using AA2.Models;
using Microsoft.EntityFrameworkCore;

namespace AA2.Data
{
    public class AA2DbContext : DbContext
    {

        public AA2DbContext(DbContextOptions<AA2DbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");

            modelBuilder.Entity<Usuario>().HasData(
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
            );

            modelBuilder.Entity<Cita>().ToTable("Citas");

            modelBuilder.Entity<Cita>().HasData(
                new Cita
                {
                    Id = 1,
                    IdUsuario = 4,
                    IdMedico = 3,
                    FechaCita = DateTime.Parse("2025-05-14T20:20:00"),
                    Motivo = "dolor",
                    Confirmada = false,
                    NombrePaciente = "juan",
                    NombreMedico = "Dr. Fernandez Santos"
                },
                new Cita
                {
                    Id = 2,
                    IdUsuario = 2,
                    IdMedico = 2,
                    FechaCita = DateTime.Parse("2025-05-16T10:32:00"),
                    Motivo = "corazon",
                    Confirmada = false,
                    NombrePaciente = "Pepe",
                    NombreMedico = "Dra. Martinez Ruiz"
                },
                new Cita
                {
                    Id = 3,
                    IdUsuario = 5,
                    IdMedico = 6,
                    FechaCita = DateTime.Parse("2025-06-20T20:01:00"),
                    Motivo = "garganta",
                    Confirmada = false,
                    NombrePaciente = "kaka",
                    NombreMedico = "lili"
                },
                new Cita
                {
                    Id = 4,
                    IdUsuario = 6,
                    IdMedico = 4,
                    FechaCita = DateTime.Parse("2025-05-20T12:02:00"),
                    Motivo = "ere",
                    Confirmada = false,
                    NombrePaciente = "ere",
                    NombreMedico = "pepe"
                },
                new Cita
                {
                    Id = 5,
                    IdUsuario = 7,
                    IdMedico = 8,
                    FechaCita = DateTime.Parse("2025-08-20T10:00:00"),
                    Motivo = "qq",
                    Confirmada = false,
                    NombrePaciente = "qq",
                    NombreMedico = "qq"
                }
            );
            modelBuilder.Entity<Medico>().ToTable("Medicos");

            modelBuilder.Entity<Medico>().HasData(
                new Medico
                {
                    Id = 1,
                    Nombre = "Dr. Garcia Lopez",
                    Especialidad = "Medicina General",
                    Email = "garcia@citasmedicas.com",
                    Telefono = "600123456",
                    FechaAlta = DateTime.Parse("2024-11-13T15:12:36.1767151+01:00"),
                    Disponible = true
                },
                new Medico
                {
                    Id = 2,
                    Nombre = "Dra. Martinez Ruiz",
                    Especialidad = "Cardiologia",
                    Email = "martinez@citasmedicas.com",
                    Telefono = "600789012",
                    FechaAlta = DateTime.Parse("2025-02-13T15:12:36.1872052+01:00"),
                    Disponible = true
                },
                new Medico
                {
                    Id = 3,
                    Nombre = "Dr. Fernandez Santos",
                    Especialidad = "Pediatria",
                    Email = "fernandez@citasmedicas.com",
                    Telefono = "600345678",
                    FechaAlta = DateTime.Parse("2025-04-13T15:12:36.1872084+02:00"),
                    Disponible = true
                }
            );
        }

    }
}