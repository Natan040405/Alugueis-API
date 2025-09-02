using Alugueis_API.Data;
using Alugueis_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Alugueis_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatariosController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;

        public LocatariosController(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddLocatario(Locatario locatario)
        {
            _AppDbContext.Locatarios.Add(locatario);
            await _AppDbContext.SaveChangesAsync();
            return Ok(locatario);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locatario>>> GetLocatarios()
        {
            List<Locatario> locatarios = await _AppDbContext.Locatarios.ToListAsync();
            return Ok(locatarios);
        }
        [HttpGet("{codLocatario}")]
        public async Task<ActionResult<Locatario>> GetLocatarioById(int codLocatario)
        {
            Locatario locatario = await _AppDbContext.Locatarios.FindAsync(codLocatario);
            if (locatario == null) return NotFound();
            return Ok(locatario);
        }
        [HttpPut("{codLocatario}")]
        public async Task<IActionResult>UpdateLocatario(int codLocatario, [FromBody] Locatario locatarioAtualizado)
        {
            Locatario locatarioAtual = await _AppDbContext.Locatarios.FindAsync(codLocatario);
            if (locatarioAtual == null) return NotFound();
            _AppDbContext.Entry(locatarioAtual).CurrentValues.SetValues(locatarioAtualizado);
            await _AppDbContext.SaveChangesAsync();
            return Ok(locatarioAtual);
        }
        [HttpDelete("{codLocatario}")]
        public async Task<IActionResult> DeleteLocatario(int codLocatario)
        {
            Locatario locatario = await _AppDbContext.Locatarios.FindAsync(codLocatario);
            if (locatario == null) return NotFound();
            _AppDbContext.Locatarios.Remove(locatario);
            await _AppDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
