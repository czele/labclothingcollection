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
    [Route("api/modelos")]
    [ApiController]
    public class ModeloController : ControllerBase
    {
        private readonly LabClothingCollectionContext _context;

        public ModeloController(LabClothingCollectionContext context)
        {
            _context = context;
        }

        // GET: api/Modelo
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? layout)
        {
          if (layout is null)
          {
              return Ok(await _context.Modelo.ToListAsync().ConfigureAwait(true));
          }

            List<Modelo> modelos = await _context.Modelo.Where(x => x.Layout == layout).ToListAsync();
            return Ok(modelos);
        }

        // GET: api/Modelo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modelo>> GetModelo(int id)
        {
          if (_context.Modelo == null)
          {
              return NotFound();
          }
            var modelo = await _context.Modelo.FindAsync(id);

            if (modelo == null)
            {
                return NotFound();
            }

            return modelo;
        }

        // PUT: api/Modelo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelo(int id, Modelo modelo)
        {
            if (id != modelo.Id)
            {
                return BadRequest();
            }

            _context.Entry(modelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModeloExists(id))
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
        public async Task<IActionResult> Post([FromBody]Modelo modelo)
        {
            var modeloExiste = await _context.Modelo.FirstOrDefaultAsync(x => x.Nome == modelo.Nome).ConfigureAwait(true);
            
            if(modeloExiste is null)
            {
                _context.Modelo.Add(modelo);

                await _context.SaveChangesAsync();

                return Ok(CreatedAtAction(nameof(Get), new { id = modelo.Id }, modelo));
            
            }

            return Conflict("Modelo já cadastrado");

        }

        // DELETE: api/Modelo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelo(int id)
        {
            if (_context.Modelo == null)
            {
                return NotFound();
            }
            var modelo = await _context.Modelo.FindAsync(id);
            if (modelo == null)
            {
                return NotFound();
            }

            _context.Modelo.Remove(modelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModeloExists(int id)
        {
            return (_context.Modelo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
