using Microsoft.EntityFrameworkCore;
using AA2.Models;

namespace AA2.Data
{
    public class  CitaEfRepository : ICitaRepository
    {
        private readonly AA2DbContext _dbcontext;

        public CitaEfRepository(AA2DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<Cita>> GetAllAsync()
        {
            return await _dbcontext.Citas.ToListAsync();
        }

        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _dbcontext.Citas.FindAsync(id);
        }

        public async Task AddAsync(Cita cita)
        {
            await _dbcontext.Citas.AddAsync(cita);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cita cita)
        {
            _dbcontext.Citas.Update(cita);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cita = await _dbcontext.Citas.FindAsync(id);
            if (cita != null) return false;
            
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

        Task ICitaRepository.DeleteAsync(int id)
        {
            return DeleteAsync(id);
        }
    }
}