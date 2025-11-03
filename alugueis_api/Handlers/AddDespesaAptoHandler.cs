using alugueis_api.Data;
using alugueis_api.Models;
using alugueis_api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alugueis_api.Handlers
{
    public class AddDespesaAptoHandler
    {
        private readonly AppDbContext _AppDbContext;
        public AddDespesaAptoHandler(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public async Task<IActionResult> Handle(DespesaAptoDTO dto) 
        {
            Despesa despesa = AddDespesa(dto);
            await _AppDbContext.SaveChangesAsync();
            await RateiaDespesa(despesa, dto.CodApto);
            await _AppDbContext.SaveChangesAsync();
            return new OkObjectResult(despesa);
        }

        private Despesa AddDespesa(DespesaAptoDTO dto)
        {
            Despesa despesa = new Despesa();
            despesa.CodDespesa = dto.CodDespesa;
            despesa.CodTipoDespesa = dto.CodTipoDespesa;
            despesa.VrlTotalDespesa = dto.VlrTotalDespesa;
            despesa.DataDespesa = DateTime.Now;
            despesa.CompetenciaMes = dto.CompetenciaMes;
            _AppDbContext.Despesas.Add(despesa);
            return despesa;
        }

        public async Task RateiaDespesa(Despesa despesa, int? codApto = 0)
        {
            List<Apto> aptos = new List<Apto>();

            if(codApto == 0)
            {
                aptos = await GetAptos();
            }
            else
            {
                aptos.Add(await GetAptoById(codApto));
            }

                foreach (Apto apto in aptos)
                {
                    DespesaRateio despesaRateio = new DespesaRateio();
                    despesaRateio.CodApto = apto.CodApto;
                    despesaRateio.CodDespesa = despesa.CodDespesa;
                    despesaRateio.VlrRateio = despesa.VrlTotalDespesa / aptos.Count;
                    _AppDbContext.DespesaRateios.Add(despesaRateio);
                }

        }


        private async Task<List<Apto>> GetAptos()
        {
            List<Apto> aptos= await _AppDbContext.Aptos.ToListAsync();
            return aptos;
        }

        private async Task<Apto> GetAptoById(int? codApto)
        {
            Apto apto = await _AppDbContext.Aptos.FindAsync(codApto);
            return apto;
        }

        public async Task<ActionResult<List<DespesaAptoDTO>>> GetDespesas()
        {
            List<DespesaAptoDTO> despesas = await _AppDbContext.Despesas
                .Include(d => d.TipoDespesa)
                .Select(d => new DespesaAptoDTO
                {
                    CodDespesa = d.CodDespesa,
                    CodTipoDespesa = d.CodTipoDespesa,
                    VlrTotalDespesa = d.VrlTotalDespesa,
                    CompetenciaMes = d.CompetenciaMes,
                    Compartilhado = d.TipoDespesa.Compartilhado,
                }).ToListAsync();
            return new OkObjectResult(despesas);
        }
    }
}
