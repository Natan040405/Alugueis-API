using alugueis_api.Data;
using alugueis_api.Models;
using alugueis_api.NovaPasta;
using Microsoft.AspNetCore.Mvc;

namespace alugueis_api.Handlers
{
    public class DeleteDespesaAptoHandler
    {
        private readonly AppDbContext _AppDbContext;
        private readonly DespesaRepository _DespesaRepository;

        public DeleteDespesaAptoHandler(AppDbContext appDbContext, DespesaRepository despesaRepository)
        {
            _AppDbContext = appDbContext;
            _DespesaRepository = despesaRepository;
        }

        public async Task<IActionResult> Handle(int codDespesa)
        {
            Despesa despesa = await _DespesaRepository.GetDespesaById(codDespesa);
            await _DespesaRepository.GetDespesaRateios(despesa);
            RemoveDespesa(despesa);
            await _DespesaRepository.SaveChangesAsync();
            return new OkObjectResult(null);
        }

        public void RemoveDespesa(Despesa despesa)
        {
            RemoveRateiosDespesa(despesa);
            _AppDbContext.Despesas.Remove(despesa);
        }
        public void RemoveRateiosDespesa(Despesa despesa)
        {
            foreach(DespesaRateio despesaRateio in despesa.Rateios)
            {
                _AppDbContext.DespesaRateios.Remove(despesaRateio);
            }
        }
    }
}
