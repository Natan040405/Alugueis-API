using alugueis_api.Data;
using alugueis_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alugueis_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDespesaController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;

        public TiposDespesaController(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> AddTipoDespesa(TipoDespesa tipoDespesa)
        {
            _AppDbContext.TiposDespesa.Add(tipoDespesa);
            await _AppDbContext.SaveChangesAsync();
            return Ok();
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
        [HttpPut("{codTipoDespesa}")]
        public async Task<IActionResult> UpdateTipoDespesa(int codTipoDespesa, [FromBody]TipoDespesa tipoDespesaAtualizado)
        {
            TipoDespesa tipoDespesaAtual = await _AppDbContext.TiposDespesa.FindAsync(codTipoDespesa);
            if(tipoDespesaAtual == null) return NotFound();
            _AppDbContext.Entry(tipoDespesaAtual).CurrentValues.SetValues(tipoDespesaAtualizado);
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
