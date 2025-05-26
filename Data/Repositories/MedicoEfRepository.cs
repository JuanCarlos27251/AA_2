using Microsoft.EntityFrameworkCore;
using AA2.Models;

namespace AA2.Data
{
    public class MedicoEfRepository : IMedicoRepository
    {
        private readonly AA2DbContext _dbcontext;

        public MedicoEfRepository(AA2DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<MedicoDtoOut> AddAsync(MedicoDtoIn medicoDto)
        {
            var medico = new Medico
            {
                Nombre = medicoDto.Nombre,
                Especialidad = medicoDto.Especialidad,
                Email = medicoDto.Email,
                Telefono = medicoDto.Telefono,
                FechaAlta = DateTime.Now,
                Disponible = medicoDto.Disponible
            };

            await _dbcontext.Medicos.AddAsync(medico);
            await _dbcontext.SaveChangesAsync();

            return new MedicoDtoOut
            {
                Nombre = medico.Nombre,
                Especialidad = medico.Especialidad,
                Email = medico.Email,
                Telefono = medico.Telefono,
                FechaAlta = medico.FechaAlta,
                Disponible = medico.Disponible
            };
        }

        public async Task<List<MedicoDtoOut>> GetAllAsync()
        {
            return await _dbcontext.Medicos
                .Select(m => new MedicoDtoOut
                {
                    Nombre = m.Nombre,
                    Especialidad = m.Especialidad,
                    Email = m.Email,
                    Telefono = m.Telefono,
                    FechaAlta = m.FechaAlta,
                    Disponible = m.Disponible
                })
                .ToListAsync();
        }

        public async Task<MedicoDtoOut?> GetByIdAsync(int id)
        {
            var medico = await _dbcontext.Medicos.FindAsync(id);
            
            if (medico == null)
                return null;

            return new MedicoDtoOut
            {
                Nombre = medico.Nombre,
                Especialidad = medico.Especialidad,
                Email = medico.Email,
                Telefono = medico.Telefono,
                FechaAlta = medico.FechaAlta,
                Disponible = medico.Disponible
            };
        }

        public async Task UpdateAsync(int id, MedicoDtoIn medicoDto)
        {
            var medico = await _dbcontext.Medicos.FindAsync(id);
            
            if (medico == null)
            {
                throw new KeyNotFoundException($"Médico con ID {id} no encontrado");
            }

            medico.Nombre = medicoDto.Nombre;
            medico.Especialidad = medicoDto.Especialidad;
            medico.Email = medicoDto.Email;
            medico.Telefono = medicoDto.Telefono;
            medico.Disponible = medicoDto.Disponible;

            _dbcontext.Medicos.Update(medico);
            await _dbcontext.SaveChangesAsync();
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var medico = await _dbcontext.Medicos.FindAsync(id);
            if (medico == null) return false;

            _dbcontext.Medicos.Remove(medico);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task InicializarDatosAsync()
        {
            if (!await _dbcontext.Medicos.AnyAsync())
            {
                var medicos = new List<Medico>
                {
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
                };

                await _dbcontext.Medicos.AddRangeAsync(medicos);
                await _dbcontext.SaveChangesAsync();
            }
        }

    }
}