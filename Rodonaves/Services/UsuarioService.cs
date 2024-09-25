using Rodonaves.Data.Repositories;
using Rodonaves.Models;
using Rodonaves.Models.ViewModels;

namespace Rodonaves.Services
{
    public interface IUsuarioService 
    {
        Task<IEnumerable<UsuarioViewModel?>> ListagemUsuarioAsync(bool status);
        Task<bool> CadastroUsuarioAsync(Usuario usuario);
        Task<Usuario> AtualizarUsuarioAsync(AtualizarUsuarioViewModel usuarioModel, int id);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;                
        }

        public async Task<IEnumerable<UsuarioViewModel?>> ListagemUsuarioAsync(bool status)
        {
            return await _usuarioRepository.ListagemUsuariosAsync(status);
        }

        public async Task<bool> CadastroUsuarioAsync(Usuario usuario)
        {
            return await _usuarioRepository.CadastroUsuarioAsync(usuario);
        }

        public async Task<Usuario> AtualizarUsuarioAsync(AtualizarUsuarioViewModel usuarioModel, int id)
        {
            return await _usuarioRepository.AtualizarUsuarioAsync(usuarioModel, id);
        }

    }
}
