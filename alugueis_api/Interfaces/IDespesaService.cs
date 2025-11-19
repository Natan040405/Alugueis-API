using alugueis_api.Models;

namespace alugueis_api.Interfaces
{
    public interface IDespesaService
    {
        void RemoveRateiosDespesaAsync(Despesa despesa);
        Task RecalculaRateiosDespesaAsync(Despesa despesa, List<Apto> aptos);
        Task RemoveDespesaAsync(Despesa despesa);
        Task<Despesa> ObterDespesaCompletaAsync(int codDespesa);
        void RateiaDespesa(Despesa despesa, List<Apto> aptos);
    }
}
