using Alugueis_API.Data;
using Alugueis_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alugueis_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrediosController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;

        public PrediosController(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult>AddPredio(Predio predio)
        {
            _AppDbContext.Predios.Add(predio);
            await _AppDbContext.SaveChangesAsync();
            return Ok(predio);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Predio>>> GetPredios()
        {
            List<Predio> predios = await _AppDbContext.Predios.ToListAsync();
            return Ok(predios);
        }
        [HttpGet("{codPredio}")]
        public async Task<ActionResult<Predio>> GetPredioById(int codPredio)
        {
            Predio predio = await _AppDbContext.Predios.FindAsync(codPredio);
            if(predio == null) return NotFound();
            return Ok(predio);
        }
    }
}
