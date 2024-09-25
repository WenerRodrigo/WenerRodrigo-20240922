using Microsoft.EntityFrameworkCore;
using Rodonaves.Models;
using Rodonaves.Models.ViewModels;

namespace Rodonaves.Data.Repositories
{
    public interface IUnidadeRepository 
    {
        Task<bool> CadastroUnidadeAsync(Unidades unidade);
        Task<Unidades?> AtualizarUsuarioAsync(AtualizarUnidadeViewModel unidadeModel, int id);
        Task<IEnumerable<Unidades>> ListagemUnidadeAsync();
    }

    public class UnidadeRepository : IUnidadeRepository
    {
        private readonly ApplicationDbContext _context;

        public UnidadeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CadastroUnidadeAsync(Unidades unidade)
        {
            await _context.Unidades.AddAsync(unidade);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Unidades?> AtualizarUsuarioAsync(AtualizarUnidadeViewModel unidadeModel, int id)
        {
            var unidade = await _context.Unidades.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (unidade == null)
            return null;

            unidade.StatusUnidade = unidadeModel.StatusUnidade;
            _context.Unidades.Update(unidade);
            await _context.SaveChangesAsync();

            return unidade;
        }

        public async Task<IEnumerable<Unidades>> ListagemUnidadeAsync()
        {
            return await _context.Unidades
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
