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
                public async Task<List<MedicoDtoOut>> GetAllAsync()
        {
            return await _medicoRepository.GetAllAsync();
        }

        public async Task<MedicoDtoOut?> GetByIdAsync(int id)
        {
            return await _medicoRepository.GetByIdAsync(id);
        }

        public async Task<List<MedicoDtoOut>> SearchAsync(
            string? nombre,
            string? especialidad,
            string orderBy,
            bool ascending)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                throw new ArgumentException("El campo de ordenamiento es obligatorio");
            }

            if (!new[] { "nombre", "especialidad", "fechaalta" }.Contains(orderBy.ToLower()))
            {
                throw new ArgumentException("Campo de ordenamiento no válido");
            }

            return await _medicoRepository.SearchAsync(nombre, especialidad, orderBy, ascending);
        }



        public async Task<MedicoDtoOut> AddAsync(MedicoDtoIn medicoDto)
        {
            return await _medicoRepository.AddAsync(medicoDto);
        }

        public async Task UpdateAsync(int id, MedicoDtoIn medicoDto)
        {
            await _medicoRepository.UpdateAsync(id, medicoDto);
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