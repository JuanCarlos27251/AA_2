using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using AA2.Data;
using AA2.Models;
using AA2.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace AA2.Services
{ 
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }
             await _usuarioRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync()
        {
            await _usuarioRepository.InicializarDatosAsync();
        }

        Task<bool> IUsuarioServices.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
