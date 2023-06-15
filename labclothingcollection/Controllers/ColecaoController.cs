using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using labclothingcollection.Context;
using labclothingcollection.Models;

namespace labclothingcollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColecaoController : ControllerBase
    {
        private readonly LabClothingCollectionContext _context;

        public ColecaoController(LabClothingCollectionContext context)
        {
            _context = context;
        }

        // GET: api/Colecao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colecao>>> GetColecao()
        {
          if (_context.Colecao == null)
          {
              return NotFound();
          }
            return await _context.Colecao.ToListAsync();
        }

        // GET: api/Colecao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colecao>> GetColecao(int id)
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

            return colecao;
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

        // POST: api/Colecao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colecao>> PostColecao(Colecao colecao)
        {
          if (_context.Colecao == null)
          {
              return Problem("Entity set 'LabClothingCollectionContext.Colecao'  is null.");
          }
            _context.Colecao.Add(colecao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColecao", new { id = colecao.Id }, colecao);
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
