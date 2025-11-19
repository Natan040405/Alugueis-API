using alugueis_api.Data;
using alugueis_api.Interfaces;
using alugueis_api.Models;
using alugueis_api.NovaPasta;
using Microsoft.AspNetCore.Mvc;

namespace alugueis_api.Handlers
{
    public class DeleteDespesaAptoHandler
    {

        private readonly IDespesaService _DespesaService;
        public DeleteDespesaAptoHandler(IDespesaService despesaService)
        {
            _DespesaService = despesaService;
        }

        public async Task<IActionResult> Handle(int codDespesa)
        {
            Despesa despesa = await _DespesaService.ObterDespesaCompletaAsync(codDespesa);
            await _DespesaService.RemoveDespesaAsync(despesa);
            return new OkObjectResult(null);
        }
    }
}
