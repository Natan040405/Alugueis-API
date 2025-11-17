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
        private readonly UpdateDespesaAptoHandler _UpdateDespesaHandler;
        private readonly DeleteDespesaAptoHandler _DeleteDespesaAptoHandler;
        private readonly GetDespesaAptoHandler _GetDespesaAptoHandler;
        private readonly AppDbContext _appDbContext;

        public DespesaAptoController(AddDespesaAptoHandler addDespesaHandler, UpdateDespesaAptoHandler updateDespesaHandler, DeleteDespesaAptoHandler deleteDespesaAptoHandler,GetDespesaAptoHandler getDespesaAptoHandler , AppDbContext appDbContext)
        {
            _AddDespesaAptoHandler = addDespesaHandler;
            _appDbContext = appDbContext;
            _UpdateDespesaHandler = updateDespesaHandler;
            _DeleteDespesaAptoHandler = deleteDespesaAptoHandler;
            _GetDespesaAptoHandler= getDespesaAptoHandler;
        }

        [HttpPost]
        public Task<IActionResult> AddDespesaApto([FromBody] AddDespesaAptoDTO dto)
        {
            return _AddDespesaAptoHandler.Handle(dto);
        }

        [HttpGet]
        public Task<ActionResult<List<GetDespesaAptoDTO>>> GetDespesas()
        {
            return _GetDespesaAptoHandler.Handle();
        }

        [HttpPut]
        public Task<IActionResult> UpdateDespesa([FromBody] AddDespesaAptoDTO dto)
        {
            return _UpdateDespesaHandler.Handle(dto);
        }

        [HttpDelete("{codDespesa}")]
        public Task<IActionResult> DeleteDespesa(int codDespesa)
        {
            return _DeleteDespesaAptoHandler.Handle(codDespesa);
        }
    }
}
