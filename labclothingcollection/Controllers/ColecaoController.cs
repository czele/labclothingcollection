using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using labclothingcollection.Context;
using labclothingcollection.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace labclothingcollection.Controllers
{
    [Route("api/colecoes")]
    [ApiController]
    public class ColecaoController : ControllerBase
    {
        private readonly LabClothingCollectionContext _context;

        public ColecaoController(LabClothingCollectionContext context)
        {
            _context = context;
        }

   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string? status)
        {
          if (status is null)
          {
                return Ok(await _context.Colecao.ToListAsync().ConfigureAwait(true));
          }
            List<Colecao> colecao = await _context.Colecao.Where(x => x.Status.ToString() == status).ToListAsync();
            
            return Ok(colecao);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var colecaoExiste = await _context.Colecao.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

            if (colecaoExiste is null)
            {
                return NotFound("Coleção não encontrado");
            }

            return Ok(colecaoExiste);

        }

        // PUT: api/Colecao/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Colecao colecao)
        {
            return NoContent();
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody]Colecao colecao)
        {
            var colecaoExiste = await _context.Colecao.FirstOrDefaultAsync(x => x.Nome == colecao.Nome).ConfigureAwait(true);

            if (colecaoExiste is null)
            {
                _context.Colecao.Add(colecao);

                await _context.SaveChangesAsync();

                return Ok(CreatedAtAction(nameof(Get), new { id = colecao.Id }, colecao));
            }

            return Conflict("Coleção já cadastrada");
            
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var colecaoExiste = await _context.Colecao.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

            if (colecaoExiste is null)
            {
                return NotFound("Coleção não encontrada");
            }

            //if(colecaoExiste.Status == "Inativo" & )

            _context.Colecao.Remove(colecaoExiste);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
