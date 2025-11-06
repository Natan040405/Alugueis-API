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

        public async Task<IActionResult> Handle(AddDespesaAptoDTO dto) 
        {
            Despesa despesa = AddDespesa(dto);
            await _AppDbContext.SaveChangesAsync();
            List<Apto> aptos = await GetAptos(dto.CodApto);
            RateiaDespesa(despesa, aptos);
            await _AppDbContext.SaveChangesAsync();
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
            _AppDbContext.Despesas.Add(despesa);
            return despesa;
        }

        public void RateiaDespesa(Despesa despesa, List<Apto> aptos)
        {
            foreach (Apto apto in aptos)
            {
                DespesaRateio despesaRateio = new DespesaRateio();
                despesaRateio.CodApto = apto.CodApto;
                despesaRateio.CodDespesa = despesa.CodDespesa;
                despesaRateio.VlrRateio = despesa.VrlTotalDespesa / aptos.Count;
                _AppDbContext.DespesaRateios.Add(despesaRateio);
            }
        }


        private async Task<List<Apto>> GetAptos(int? codApto = 0)
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


        private async Task<Apto> GetAptoById(int? codApto)
        {
            Apto apto = await _AppDbContext.Aptos.FindAsync(codApto);
            return apto;
        }

        private async Task<List<DespesaRateio>> GetDespesaRateios(int codDespesaRateio = 0)
        {
            List<DespesaRateio> despesaRateios = new List<DespesaRateio>();

            if (codDespesaRateio == 0)
            {
                despesaRateios = await _AppDbContext.DespesaRateios.ToListAsync();
            }
            else
            {
                despesaRateios.Add(await GetDespesaRateioById(codDespesaRateio));
            }

            return despesaRateios;
        }

        private async Task<DespesaRateio> GetDespesaRateioById(int codDespesaRateio)
        {
            DespesaRateio tipoDespesa = await _AppDbContext.DespesaRateios.FindAsync(codDespesaRateio);
            return tipoDespesa;
        }

        private async Task<TipoDespesa> GetTipoDespesaById(int codTipoDespesa)
        {
            TipoDespesa tipoDespesa = await _AppDbContext.TiposDespesa.FindAsync(codTipoDespesa);
            return tipoDespesa;
        }

        public async Task<ActionResult<List<GetDespesaAptoDTO>>> GetDespesas()
        {
            List<GetDespesaAptoDTO> despesas = await _AppDbContext.Despesas
                .Include(d => d.TipoDespesa)
                .Select(d => new GetDespesaAptoDTO(
                    d.CodDespesa,
                    d.CodTipoDespesa,
                    d.TipoDespesa.NomeTipoDespesa,
                    d.VrlTotalDespesa,
                    d.DataDespesa,
                    d.CompetenciaMes,
                    d.TipoDespesa.Compartilhado
                )).ToListAsync();
            return new OkObjectResult(despesas);
        }
    }
}
