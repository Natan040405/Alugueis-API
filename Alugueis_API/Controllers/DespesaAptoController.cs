using alugueis_api.Data;
using alugueis_api.Handlers;
using alugueis_api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alugueis_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaAptoController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;
        private readonly AddDespesaAptoHandler _AddDespesaAptoHandler;

        public DespesaAptoController(AppDbContext appDbContext, AddDespesaAptoHandler handler)
        {
            _AppDbContext = appDbContext;
            _AddDespesaAptoHandler = handler;
        }

        [HttpPost]
        public Task<IActionResult> AddDespesaApto([FromBody] AddDespesaAptoDTO dto)
        {
            return _AddDespesaAptoHandler.Handle(dto);
        }
    }
}
