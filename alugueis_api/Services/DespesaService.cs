using alugueis_api.Interfaces;
using alugueis_api.Models;
using alugueis_api.NovaPasta;

namespace alugueis_api.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly DespesaRepository _DespesaRepository;

        public DespesaService(DespesaRepository despesaRepository)
        {
            _DespesaRepository = despesaRepository;
        }

        public async Task<Despesa> ObterDespesaCompletaAsync(int codDespesa)
        {
            Despesa despesa = await _DespesaRepository.GetDespesaById(codDespesa);
            await _DespesaRepository.GetDespesaRateios(despesa);
            return despesa;
        }

        public async Task RecalculaRateiosDespesaAsync(Despesa despesa, List<Apto> aptos)
        {
            RemoveRateiosDespesaAsync(despesa);
            RateiaDespesa(despesa, aptos);
            await _DespesaRepository.SaveChangesAsync();
        }
        public void RateiaDespesa(Despesa despesa, List<Apto> aptos)
        {
            float valorRateio = despesa.VrlTotalDespesa / aptos.Count;
            foreach (Apto apto in aptos)
            {
                DespesaRateio despesaRateio = new DespesaRateio();
                despesaRateio.CodApto = apto.CodApto;
                despesaRateio.CodDespesa = despesa.CodDespesa;
                despesaRateio.VlrRateio = valorRateio;
                _DespesaRepository.AddRateio(despesaRateio);
            }
        }

        public async Task RemoveDespesaAsync(Despesa despesa)
        {
            RemoveRateiosDespesaAsync(despesa);
            _DespesaRepository.Remove(despesa);
            await _DespesaRepository.SaveChangesAsync();
        }


        public void RemoveRateiosDespesaAsync(Despesa despesa)
        {
            foreach (DespesaRateio despesaRateio in despesa.Rateios)
            {
                _DespesaRepository.RemoveRateio(despesaRateio);
            }
        }
    }
}
