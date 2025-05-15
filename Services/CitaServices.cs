using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using AA2.Data;
using AA2.Models;
using AA2.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AA2.Services
{
    public class CitaServices : ICitaServices
    {
        private readonly ICitaRepository _citaRepository;

        public CitaServices(ICitaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        public async Task<List<Cita>> GetAllAsync()
        {
            return await _citaRepository.GetAllAsync();
        }

        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _citaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Cita cita)
        {
            await _citaRepository.AddAsync(cita);
        }

        public async Task UpdateAsync(Cita cita)
        {
            await _citaRepository.UpdateAsync(cita);
        }

        public async Task DeleteAsync(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            if (cita == null)
            {
                throw new KeyNotFoundException("Cita no encontrada");
            }
             await _citaRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync()
        {
            await _citaRepository.InicializarDatosAsync();
        }

        Task<bool> ICitaServices.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }


}