using alugueis_api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alugueis_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaAptoController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;

        public DespesaAptoController(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddDespesaApto([FromBody] )
    }
}
