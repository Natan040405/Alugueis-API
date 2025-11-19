using alugueis_api.Data;
using alugueis_api.Models;
using Microsoft.EntityFrameworkCore;

namespace alugueis_api.Repositories
{
    public class AptoRepository
    {
        private readonly AppDbContext _AppDbContext;
        public AptoRepository(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public async Task<List<Apto>> GetAptos(int? codApto = 0)
        {
            List<Apto> aptos = new List<Apto>();

            if (codApto == 0 || codApto == null)
            {
                aptos = await _AppDbContext.Aptos.ToListAsync();
            }
            else
            {
                aptos.Add(await GetAptoById(codApto));
            }

            return aptos;
        }

        public async Task<Apto> GetAptoById(int? codApto)
        {
            Apto apto = await _AppDbContext.Aptos.FindAsync(codApto);
            return apto;
        }
    }
}
