using Microsoft.EntityFrameworkCore;
using Rodonaves.Models;
using Rodonaves.Models.ViewModels;

namespace Rodonaves.Data.Repositories
{
    public interface IUsuarioRepository 
    {
        Task<IEnumerable<UsuarioViewModel>> ListagemUsuariosAsync(bool status);
        Task<bool> CadastroUsuarioAsync(Usuario usuario);
        Task<Usuario> AtualizarUsuarioAsync(AtualizarUsuarioViewModel usuarioModel, int id);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioViewModel>> ListagemUsuariosAsync(bool status)
        {
            var usuario = await _context.Usuarios.Where(x => x.Status == status).ToListAsync();

            var usuarioModel = new List<UsuarioViewModel>();
            foreach (var item in usuario) 
            {
                var usuarioView = new UsuarioViewModel()
                {
                    Login = item.Login,
                    status = item.Status,
                };                
                usuarioModel.Add(usuarioView);
            }

            return usuarioModel;
        }

        public async Task<bool> CadastroUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario> AtualizarUsuarioAsync(AtualizarUsuarioViewModel usuarioModel, int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == id);
            if (usuario == null) { return null; }

            if(!string.IsNullOrEmpty(usuarioModel.Senha))
                usuario.Senha = usuarioModel.Senha;
                usuario.Status = usuarioModel.Status;
            await _context.SaveChangesAsync();
            return usuario;
        }

    }
}
