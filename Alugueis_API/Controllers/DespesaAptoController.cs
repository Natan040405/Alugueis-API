using alugueis_api.Data;
using alugueis_api.Handlers;
using alugueis_api.Models;
using alugueis_api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alugueis_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaAptoController : ControllerBase
    {
        private readonly AddDespesaAptoHandler _AddDespesaAptoHandler;
        private readonly AppDbContext _appDbContext;

        public DespesaAptoController(AddDespesaAptoHandler handler, AppDbContext appDbContext)
        { 
            _AddDespesaAptoHandler = handler;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public Task<IActionResult> AddDespesaApto([FromBody] AddDespesaAptoDTO dto)
        {
            return _AddDespesaAptoHandler.Handle(dto);
        }

        [HttpGet]
        public Task<ActionResult<List<GetDespesaAptoDTO>>> GetDespesas()
        {
            return _AddDespesaAptoHandler.GetDespesas();
        }
    }
}
