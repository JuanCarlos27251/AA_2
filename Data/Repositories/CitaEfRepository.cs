using Microsoft.EntityFrameworkCore;
using AA2.Models;

namespace AA2.Data
{
    public class CitaEfRepository : ICitaRepository
    {
        private readonly AA2DbContext _dbcontext;

        public CitaEfRepository(AA2DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<CitaDtoOut>> GetAllAsync()
        {
            return await _dbcontext.Citas
                .Select(c => new CitaDtoOut
                {
                    Id = c.Id,
                    IdUsuario = c.IdUsuario,
                    IdMedico = c.IdMedico,
                    NombrePaciente = c.NombrePaciente,
                    NombreMedico = c.NombreMedico,
                    FechaCita = c.FechaCita,
                    Motivo = c.Motivo,
                    Confirmada = c.Confirmada
                })
                .ToListAsync();
        }

        public async Task<CitaDtoOut?> GetByIdAsync(int id)
        {
            var cita = await _dbcontext.Citas.FindAsync(id);
            
            if (cita == null)
                return null;

            return new CitaDtoOut
            {
                Id = cita.Id,
                IdUsuario = cita.IdUsuario,
                IdMedico = cita.IdMedico,
                NombrePaciente = cita.NombrePaciente,
                NombreMedico = cita.NombreMedico,
                FechaCita = cita.FechaCita,
                Motivo = cita.Motivo,
                Confirmada = cita.Confirmada
            };
        }

                public IQueryable<Cita> GetQueryable()
        {
            return _dbcontext.Citas.AsQueryable();
        }

        public async Task<List<CitaDtoOut>> SearchAsync(
            int userId,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string? nombreMedico,
            string orderBy,
            bool ascending)
        {
            var query = GetQueryable().Where(c => c.IdUsuario == userId);

            if (fechaInicio.HasValue)
                query = query.Where(c => c.FechaCita >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(c => c.FechaCita <= fechaFin.Value);

            if (!string.IsNullOrEmpty(nombreMedico))
                query = query.Where(c => c.NombreMedico.Contains(nombreMedico));

            query = orderBy.ToLower() switch
            {
                "fecha" => ascending ? query.OrderBy(c => c.FechaCita)
                                   : query.OrderByDescending(c => c.FechaCita),
                "medico" => ascending ? query.OrderBy(c => c.NombreMedico)
                                    : query.OrderByDescending(c => c.NombreMedico),
                _ => query.OrderBy(c => c.FechaCita)
            };

            return await query.Select(c => new CitaDtoOut
            {
                Id = c.Id,
                IdUsuario = c.IdUsuario,
                NombrePaciente = c.NombrePaciente,
                NombreMedico = c.NombreMedico,
                FechaCita = c.FechaCita,
                Motivo = c.Motivo,
                Confirmada = c.Confirmada
            }).ToListAsync();
        }
        
        public async Task<CitaDtoOut> AddAsync(CitaDtoIn citaDtoIn)
        {
            // Buscar IDs basados en los nombres
            var usuario = await _dbcontext.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == citaDtoIn.NombrePaciente);
            var medico = await _dbcontext.Medicos
                .FirstOrDefaultAsync(m => m.Nombre == citaDtoIn.NombreMedico);

            if (usuario == null || medico == null)
            {
                throw new KeyNotFoundException("Paciente o médico no encontrado");
            }

            var cita = new Cita
            {
                IdUsuario = usuario.Id,
                IdMedico = medico.Id,
                FechaCita = citaDtoIn.FechaCita,
                Motivo = citaDtoIn.Motivo,
                Confirmada = false,
                NombrePaciente = citaDtoIn.NombrePaciente,
                NombreMedico = citaDtoIn.NombreMedico
            };

            await _dbcontext.Citas.AddAsync(cita);
            await _dbcontext.SaveChangesAsync();

            return new CitaDtoOut
            {
                Id = cita.Id,
                IdUsuario = cita.IdUsuario,
                IdMedico = cita.IdMedico,
                NombrePaciente = cita.NombrePaciente,
                NombreMedico = cita.NombreMedico,
                FechaCita = cita.FechaCita,
                Motivo = cita.Motivo,
                Confirmada = cita.Confirmada
            };
        }
        
        public async Task UpdateAsync(int id, CitaDtoIn citaDtoIn)
        {
            var cita = await _dbcontext.Citas.FindAsync(id);
            
            if (cita == null)
            {
                throw new KeyNotFoundException($"Cita con ID {id} no encontrada");
            }

            // Buscar IDs basados en los nombres
            var usuario = await _dbcontext.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == citaDtoIn.NombrePaciente);
            var medico = await _dbcontext.Medicos
                .FirstOrDefaultAsync(m => m.Nombre == citaDtoIn.NombreMedico);

            if (usuario == null || medico == null)
            {
                throw new KeyNotFoundException("Paciente o médico no encontrado");
            }

            // Actualizar los campos de la cita
            cita.IdUsuario = usuario.Id;
            cita.IdMedico = medico.Id;
            cita.FechaCita = citaDtoIn.FechaCita;
            cita.Motivo = citaDtoIn.Motivo;
            cita.NombrePaciente = citaDtoIn.NombrePaciente;
            cita.NombreMedico = citaDtoIn.NombreMedico;

            if (citaDtoIn.Confirmada.HasValue)
            {
                cita.Confirmada = citaDtoIn.Confirmada.Value;
            }

            _dbcontext.Citas.Update(cita);
            await _dbcontext.SaveChangesAsync();
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var cita = await _dbcontext.Citas.FindAsync(id);
            if (cita == null)
            {
                return false;
            }

            _dbcontext.Citas.Remove(cita);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task InicializarDatosAsync()
        {
            if (!await _dbcontext.Citas.AnyAsync())
            {
                var citas = new List<Cita>
                {
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
                };

                await _dbcontext.Citas.AddRangeAsync(citas);
                await _dbcontext.SaveChangesAsync();
            }
        }


    }
}