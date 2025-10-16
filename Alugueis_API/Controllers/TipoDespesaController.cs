using alugueis_api.Data;
using alugueis_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alugueis_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDespesaController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;

        public TipoDespesaController(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> AddTipoDespesa(TipoDespesa tipoDespesa)
        {
            _AppDbContext.TiposDespesa.Add(tipoDespesa);
            await _AppDbContext.SaveChangesAsync();
            return Ok(tipoDespesa);
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<TipoDespesa>>> GetTiposDespesa()
        {
            List<TipoDespesa> tiposDespesa = await _AppDbContext.TiposDespesa.ToListAsync();
            return Ok(tiposDespesa);
        }
        [HttpGet("{codTipoDespesa}")]
        public async Task<ActionResult<TipoDespesa>> GetTipoDespesaById(int codTipoDespesa)
        {
            TipoDespesa tipoDespesa = await _AppDbContext.TiposDespesa.FindAsync(codTipoDespesa);
            if (tipoDespesa == null) return NotFound();
            return Ok(tipoDespesa);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTipoDespesa([FromBody]TipoDespesa tipoDespesaAtualizado)
        {
            TipoDespesa tipoDespesaAtual = await _AppDbContext.TiposDespesa.FindAsync(tipoDespesaAtualizado.CodTipo);
            if(tipoDespesaAtual == null) return NotFound();
            _AppDbContext.Entry(tipoDespesaAtual).CurrentValues.SetValues(tipoDespesaAtualizado);
            await _AppDbContext.SaveChangesAsync();
            return Ok(tipoDespesaAtualizado);
        }
        [HttpDelete("{codTipoDespesa}")]
        public async Task<IActionResult> DeleteTipoDespesa(int codTipoDespesa)
        {
            TipoDespesa tipoDespesa = await _AppDbContext.TiposDespesa.FindAsync(codTipoDespesa);
            if (tipoDespesa == null) return NotFound();
            _AppDbContext.TiposDespesa.Remove(tipoDespesa);
            await _AppDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
