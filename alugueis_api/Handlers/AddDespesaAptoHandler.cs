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
            await RateiaDespesa(dto);
            await _AppDbContext.SaveChangesAsync();
            return new OkObjectResult("Despesa Cadastrada com Sucesso");
        }

        private Despesa AddDespesa(AddDespesaAptoDTO dto)
        {
            Despesa despesa = new Despesa();
            despesa.CodDespesa = dto.CodDespesa;
            despesa.CodTipoDespesa = dto.CodTipoDespesa;
            despesa.VrlTotalDespesa = dto.VlrTotalDespesa;
            despesa.DataDespesa = dto.DataDespesa;
            despesa.CompetenciaMes = dto.CompetenciaMes;
            _AppDbContext.Despesas.Add(despesa);
            return despesa;
        }

        public async Task RateiaDespesa(AddDespesaAptoDTO dto)
        {
            List<Apto> aptos = new List<Apto>();

            if (dto.Compartilhado == 1)
            {
                aptos = await GetAptos();
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
    }
}
