using alugueis_api.Data;
using alugueis_api.Interfaces;
using alugueis_api.Models;
using alugueis_api.NovaPasta;
using alugueis_api.Repositories;

namespace alugueis_api.Handlers
{
    public class RecalculationDespesaRateioHandler
    {
        private readonly DespesaRepository _DespesaRepository;
        private readonly AptoRepository _AptoRepository;
        private readonly IDespesaService _DespesaService;

        public RecalculationDespesaRateioHandler(DespesaRepository despesaRepository, AptoRepository aptoRepository, IDespesaService despesaService)
        {
            _DespesaRepository = despesaRepository;
            _AptoRepository = aptoRepository;
            _DespesaService = despesaService;
        }

        public async Task Handle(int codDespesa)
        {
            Despesa despesa = await _DespesaService.ObterDespesaCompletaAsync(codDespesa);
            List<Apto> aptos = await _AptoRepository.GetAptos();
            await _DespesaService.RecalculaRateiosDespesaAsync(despesa, aptos);
        }
    }
}
