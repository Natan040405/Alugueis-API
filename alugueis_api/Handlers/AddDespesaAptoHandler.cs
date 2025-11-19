using alugueis_api.Data;
using alugueis_api.Models;
using alugueis_api.Models.DTOs;
using alugueis_api.NovaPasta;
using alugueis_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alugueis_api.Handlers
{
    public class AddDespesaAptoHandler
    {
        private readonly AppDbContext _AppDbContext;
        private readonly DespesaRepository _DespesaRepository;
        private readonly AptoRepository _AptoRepository;
        public AddDespesaAptoHandler(AppDbContext appDbContext, DespesaRepository despesaRepository, AptoRepository aptoRepository)
        {
            _AppDbContext = appDbContext;
            _DespesaRepository = despesaRepository;
            _AptoRepository = aptoRepository;
        }

        public async Task<IActionResult> Handle(AddDespesaAptoDTO dto) 
        {
            Despesa despesa = AddDespesa(dto);
            await _DespesaRepository.SaveChangesAsync();
            List<Apto> aptos = await _AptoRepository.GetAptos(dto.CodApto);
            RateiaDespesa(despesa, aptos);
            await _DespesaRepository.SaveChangesAsync();
            despesa.TipoDespesa = await GetTipoDespesaById(despesa.CodTipoDespesa);
            GetDespesaAptoDTO getDespesaAptoDTO = new GetDespesaAptoDTO(
                despesa.CodDespesa,
                despesa.CodTipoDespesa,
                despesa.TipoDespesa.NomeTipoDespesa,
                despesa.VrlTotalDespesa,
                despesa.DataDespesa,
                despesa.CompetenciaMes,
                despesa.TipoDespesa.Compartilhado
                );
            return new OkObjectResult(getDespesaAptoDTO);
        }

        private Despesa AddDespesa(AddDespesaAptoDTO dto)
        {
            Despesa despesa = new Despesa();
            despesa.CodDespesa = dto.CodDespesa;
            despesa.CodTipoDespesa = dto.CodTipoDespesa;
            despesa.VrlTotalDespesa = dto.VlrTotalDespesa;
            despesa.DataDespesa = DateTime.Now;
            despesa.CompetenciaMes = dto.CompetenciaMes;
            _DespesaRepository.Add(despesa);
            return despesa;
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


       

        private async Task<TipoDespesa> GetTipoDespesaById(int codTipoDespesa)
        {
            TipoDespesa tipoDespesa = await _AppDbContext.TiposDespesa.FindAsync(codTipoDespesa);
            return tipoDespesa;
        }
    }
}
