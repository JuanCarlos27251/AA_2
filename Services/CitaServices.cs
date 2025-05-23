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

        public async Task<List<CitaDtoOut>> GetAllAsync()
        {
            return await _citaRepository.GetAllAsync();
        }

        public async Task<CitaDtoOut?> GetByIdAsync(int id)
        {
            return await _citaRepository.GetByIdAsync(id);
        }

        public async Task<CitaDtoOut> AddAsync(CitaDtoIn citaDtoIn)
        {
            return await _citaRepository.AddAsync(citaDtoIn);
        }

        public async Task UpdateAsync(int id, CitaDtoIn citaDtoIn)
        {
            await _citaRepository.UpdateAsync(id, citaDtoIn);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _citaRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync()
        {
            await _citaRepository.InicializarDatosAsync();
        }

        // public async Task<List<Cita>> GetAllAsync()
        // {
        //     return await _citaRepository.GetAllAsync();
        // }

        // public async Task<Cita?> GetByIdAsync(int id)
        // {
        //     return await _citaRepository.GetByIdAsync(id);
        // }

        // public async Task AddAsync(CitaDtoIn cita)
        // {
        //     await _citaRepository.AddAsync(cita);
        // }

        // public async Task UpdateAsync(Cita cita)
        // {
        //     await _citaRepository.UpdateAsync(cita);
        // }

        // public async Task<bool> DeleteAsync(int id)
        // {
        //     return await _citaRepository.DeleteAsync(id);
        // }

        // public async Task InicializarDatosAsync()
        // {
        //     await _citaRepository.InicializarDatosAsync();
        // }
    }


}