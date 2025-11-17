using alugueis_api.Data;
using alugueis_api.Models;
using alugueis_api.Models.DTOs;
using alugueis_api.NovaPasta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alugueis_api.Handlers
{
    public class UpdateDespesaAptoHandler
    {
        private readonly AppDbContext _AppDbContext;
        private readonly DespesaRepository _DespesaRepository;

        public UpdateDespesaAptoHandler(AppDbContext appDbContext, DespesaRepository despesaRepository)
        {
            _AppDbContext = appDbContext;
            _DespesaRepository = despesaRepository;
        }

        public async Task<IActionResult> Handle(AddDespesaAptoDTO dto)
        {
            Despesa despesa = await _DespesaRepository.GetDespesaById(dto.CodDespesa);
            await _DespesaRepository.GetDespesaRateios(despesa);
            await _DespesaRepository.GetTipoDespesaDespesa(despesa);
            UpdateDespesa(despesa, dto);
            await _AppDbContext.SaveChangesAsync();
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

        private void UpdateDespesa(Despesa despesa, AddDespesaAptoDTO dto)
        {
            despesa.VrlTotalDespesa = dto.VlrTotalDespesa;
            despesa.CompetenciaMes = dto.CompetenciaMes;
            UpdateDespesaRateios(despesa);
        }

        private void UpdateDespesaRateios(Despesa despesa)
        {
            float valorRateio = despesa.VrlTotalDespesa / despesa.Rateios.Count;
            foreach (DespesaRateio rateio in despesa.Rateios)
            {
                rateio.VlrRateio = valorRateio;
            }
        }

    }
}
