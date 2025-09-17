using alugueis_api.Data;
using alugueis_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace alugueis_api.Controllers
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
        public async Task<IActionResult> AddLocatario([FromBody] Locatario locatario)
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
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Locatario>> GetLocatarioById(string cpf)
        {
            Locatario locatario = await _AppDbContext.Locatarios.FindAsync(cpf);
            if (locatario == null) return NotFound();
            return Ok(locatario);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateLocatario([FromBody] Locatario locatarioAtualizado)
        {
            Locatario locatarioAtual = await _AppDbContext.Locatarios.FindAsync(locatarioAtualizado.Cpf);
            if (locatarioAtual == null) return NotFound();
            _AppDbContext.Entry(locatarioAtual).CurrentValues.SetValues(locatarioAtualizado);
            await _AppDbContext.SaveChangesAsync();
            return Ok(locatarioAtual);
        }
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteLocatario(string cpf)
        {
            Locatario locatario = await _AppDbContext.Locatarios.FindAsync(cpf);
            if (locatario == null) return NotFound();
            _AppDbContext.Locatarios.Remove(locatario);
            await _AppDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
