using alugueis_api.Data;
using alugueis_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace alugueis_api.NovaPasta
{
    public class DespesaRepository
    {
        private readonly AppDbContext _AppDbContext;

        public DespesaRepository(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public async Task<Despesa> GetDespesaById(int? codDespesa)
        {
            Despesa despesa = await _AppDbContext.Despesas.FindAsync(codDespesa);
            return despesa;
        }
        public async Task GetDespesaRateios(Despesa despesa)
        {
            await _AppDbContext.Entry(despesa).Collection(d => d.Rateios).LoadAsync();
        }
        public async Task GetTipoDespesaDespesa(Despesa despesa)
        {
            await _AppDbContext.Entry(despesa).Reference(d => d.TipoDespesa).LoadAsync();
        }

        public async Task GetTipoDespesaDespesas(List<Despesa> despesas)
        {

            List<Despesa> despesasComTipo = await _AppDbContext.Despesas
            .Include(d => d.TipoDespesa)
            .ToListAsync();

            foreach (var despesa in despesas)
            {
                despesa.TipoDespesa = despesasComTipo
                    .FirstOrDefault(x => x.CodDespesa == despesa.CodDespesa)
                    .TipoDespesa;
            }
        }
        public async Task<List<Despesa>> GetDespesas(int? codDespesa = 0)
        {
            List<Despesa> despesas = new List<Despesa>();

            if (codDespesa == 0 || codDespesa == null)
            {
                despesas = await _AppDbContext.Despesas.ToListAsync();
            }
            else
            {
                despesas.Add(await GetDespesaById(codDespesa));
            }

            return despesas;
        }
        public async Task SaveChangesAsync()
        {
            await _AppDbContext.SaveChangesAsync();
        }
    }
}
