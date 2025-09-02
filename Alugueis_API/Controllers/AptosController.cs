using Alugueis_API.Data;
using Alugueis_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alugueis_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AptosController : ControllerBase
    {
        //Cria objeto de referencia ao banco de dados
        private readonly AppDbContext _AppDbContext;

        //Constructor da classe gerando o banco no objeto de referencia
        public AptosController(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddAptos(Apto apto)
        {
            _AppDbContext.Aptos.Add(apto);
            await _AppDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apto>>> GetAptos()
        {
            List<Apto> aptos = await _AppDbContext.Aptos.ToListAsync();
            return Ok(aptos);
        }
        [HttpGet("{codApto}")]
        public async Task<ActionResult<Apto>>GetAptoById(int codApto)
        {
            Apto apto = await _AppDbContext.Aptos.FindAsync(codApto);
            if (apto == null) return NotFound();
            return Ok(apto);
        }
        [HttpPut("{codApto}")]
        public async Task<IActionResult>UpdateApto(int codApto, [FromBody] Apto aptoAtualizado)
        {
            Apto aptoAtual = await _AppDbContext.Aptos.FindAsync(codApto);
            if(aptoAtual == null) return NotFound();
            _AppDbContext.Entry(aptoAtual).CurrentValues.SetValues(aptoAtualizado);
            await _AppDbContext.SaveChangesAsync();
            return Ok(aptoAtual);
        }
        [HttpDelete("{codApto}")]
        public async Task<IActionResult>DeleteApto(int codApto)
        {
            Apto apto = await _AppDbContext.Aptos.FindAsync(codApto);
            if (apto == null) return NotFound();
            _AppDbContext.Aptos.Remove(apto);
            await _AppDbContext.SaveChangesAsync();
            return NoContent();
        }



    }
}
