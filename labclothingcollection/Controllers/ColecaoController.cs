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
            List<Colecao> colecao = await _context.Colecao.Where(x => x.Status == status).ToListAsync();
            
            return Ok(colecao);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var colecao = await _context.Colecao.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

            if (colecao is null)
            {
                return NotFound("Coleção não encontrado");
            }

            return Ok(colecao);

        }

        // PUT: api/Colecao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColecao(int id, Colecao colecao)
        {
            if (id != colecao.Id)
            {
                return BadRequest();
            }

            _context.Entry(colecao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColecaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

        // DELETE: api/Colecao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColecao(int id)
        {
            if (_context.Colecao == null)
            {
                return NotFound();
            }
            var colecao = await _context.Colecao.FindAsync(id);
            if (colecao == null)
            {
                return NotFound();
            }

            _context.Colecao.Remove(colecao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColecaoExists(int id)
        {
            return (_context.Colecao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
