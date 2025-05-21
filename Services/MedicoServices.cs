using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using AA2.Data;
using AA2.Models;
using AA2.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace AA2.Services
{
    public class MedicoServices : IMedicoServices
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoServices(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<List<Medico>> GetAllAsync()
        {
            return await _medicoRepository.GetAllAsync();
        }

        public async Task<Medico?> GetByIdAsync(int id)
        {
            return await _medicoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Medico medico)
        {
            await _medicoRepository.AddAsync(medico);
        }

        public async Task UpdateAsync(Medico medico)
        {
            await _medicoRepository.UpdateAsync(medico);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _medicoRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync()
        {
            await _medicoRepository.InicializarDatosAsync();
        }
    }
}