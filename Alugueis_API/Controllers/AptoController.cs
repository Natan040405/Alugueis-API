﻿using alugueis_api.Data;
using alugueis_api.Models;
using alugueis_api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alugueis_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AptoController : ControllerBase
    {
        //Cria objeto de referencia ao banco de dados
        private readonly AppDbContext _AppDbContext;

        //Constructor da classe gerando o banco no objeto de referencia
        public AptoController(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddAptos(Apto apto)
        {
            _AppDbContext.Aptos.Add(apto);
            await _AppDbContext.SaveChangesAsync();
            AptoListDTO aptoDTO = await _AppDbContext.Aptos
                .Include(a => a.Predio)
                .Where(a => a.CodApto == apto.CodApto)
                .Select(a => new AptoListDTO
                {
                    CodApto = a.CodApto,
                    CodPredio = a.CodPredio,
                    NomePredio = a.Predio.NomePredio,
                    Andar = a.Andar,
                    QtdQuartos = a.QtdQuartos,
                    QtdBanheiros = a.QtdBanheiros,
                    MetrosQuadrados = a.MetrosQuadrados,
                })
                .FirstOrDefaultAsync();
            return Ok(aptoDTO);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AptoListDTO>>> GetAptos()
        {
            List<AptoListDTO> aptos = await _AppDbContext.Aptos
                .Include(a => a.Predio)
                .Select( a => new AptoListDTO
                {
                    CodApto = a.CodApto,
                    CodPredio = a.CodPredio,
                    NomePredio = a.Predio.NomePredio,
                    Andar = a.Andar,
                    QtdQuartos = a.QtdQuartos,
                    QtdBanheiros = a.QtdBanheiros,
                    MetrosQuadrados = a.MetrosQuadrados,
                })
                .ToListAsync();
            return Ok(aptos);
        }
        [HttpGet("{codApto}")]
        public async Task<ActionResult<Apto>>GetAptoById(int codApto)
        {
            Apto apto = await _AppDbContext.Aptos.FindAsync(codApto);
            if (apto == null) return NotFound();
            return Ok(apto);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateApto([FromBody] Apto aptoAtualizado)
        {
            Apto aptoAtual = await _AppDbContext.Aptos.FindAsync(aptoAtualizado.CodApto);
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
