using Microsoft.EntityFrameworkCore;
using Rodonaves.Models;
using Rodonaves.Models.ViewModels;

namespace Rodonaves.Data.Repositories
{

    public interface IColaboradorRepository 
    {
        Task<IEnumerable<Colaboradores>> ListagemColaboradoresAsync();
        Task<Colaboradores> CadastroColaboradorAsync(Colaboradores colaborador);
        Task<Colaboradores?> AtualizarColaboradorAsync(int id, AtualizarColaboradorViewModel colaboradorModel);
        Task<Colaboradores?> DeletarColaboradorAsync(int id);
    }

    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly ApplicationDbContext _context;

        public ColaboradorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Colaboradores>> ListagemColaboradoresAsync() 
        {
            return await _context.Colaboradores
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Colaboradores> CadastroColaboradorAsync(Colaboradores colaborador) 
        {
            var uni = _context.Unidades.FirstOrDefaultAsync(x => x.Codigo == colaborador.UniCodigo && x.StatusUnidade);
            if (uni == null) return null; 
            await _context.Colaboradores.AddAsync(colaborador);
            await _context.SaveChangesAsync();
            return colaborador;
        }

        public async Task<Colaboradores?> AtualizarColaboradorAsync(int id, AtualizarColaboradorViewModel colaboradorModel)
        {
            var colaborador = await _context.Colaboradores.FirstOrDefaultAsync(x => x.Id == id);
            if (colaborador == null) return null;

            var unidade = await _context.Unidades.ToListAsync();
            var colaboradorUnidade = unidade.FirstOrDefault(x => x.Id == colaboradorModel.UniCodigo);
            if(colaboradorUnidade == null) return null;

            if(!string.IsNullOrEmpty(colaboradorModel.Nome))
                colaborador.Nome = colaboradorModel.Nome;
                colaborador.UniCodigo = colaboradorModel.UniCodigo;

            await _context.SaveChangesAsync();
            return colaborador;
        }

        public async Task<Colaboradores?> DeletarColaboradorAsync(int id) 
        {
            var result = await _context.Colaboradores.FirstOrDefaultAsync(c => c.Id == id);
            if(result == null) return null;

            _context.Colaboradores.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }        
    }
}
