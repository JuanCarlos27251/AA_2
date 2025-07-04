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

        public IEnumerable<UsuarioDtoOut> GetAll()
        {
            return _usuarioRepository.GetAll();
        }

        public async Task<List<UsuarioDtoOut>> SearchAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string orderBy,
            bool ascending)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                throw new ArgumentException("El campo de ordenamiento es obligatorio");
            }

            if (!new[] { "fecha", "nombre" }.Contains(orderBy.ToLower()))
            {
                throw new ArgumentException("Campo de ordenamiento no válido");
            }

            return await _usuarioRepository.SearchAsync(
                fechaInicio,
                fechaFin,
                orderBy,
                ascending);
        }

        public void Add(UsuarioDtoin usuario)
        {
            _usuarioRepository.Add(usuario);
        }
        
        public void Update(int id, UsuarioDtoin usuario)
        {
            _usuarioRepository.Update(id, usuario);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _usuarioRepository.DeleteAsync(id);
        }


        public async Task<UsuarioDtoOut> GetUserFromCredentialsAsync(LoginDtoIn loginDtoIn)
        {
            return await Task.Run(() => _usuarioRepository.GetUserFromCredentials(loginDtoIn));
        }
        public async Task<UsuarioDtoOut> AddUserFromCredentialsAsync(UsuarioDtoin usuarioDtoin)
        {
            return await Task.Run(() => _usuarioRepository.AddUserFromCredentials(usuarioDtoin));
        }
        
        public async Task InicializarDatosAsync()
        {
            await _usuarioRepository.InicializarDatosAsync();
        }

  
    }
}
