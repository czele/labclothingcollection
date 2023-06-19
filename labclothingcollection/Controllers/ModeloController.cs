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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string? layout)
        {
          if (layout is null)
          {
              return Ok(await _context.Modelo.ToListAsync().ConfigureAwait(true));
          }

            List<Modelo> modelos = await _context.Modelo.Where(x => x.Layout == layout).ToListAsync();
            
            return Ok(modelos);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Modelo>> Get(int id)
        {

            var modeloExiste = await _context.Modelo.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);
           
            if(modeloExiste is null) 
            {
                return NotFound("Modelo não encontrado");
            }
            
            return Ok(modeloExiste);
        }

        // PUT: api/Modelo/5

        [HttpPut("id")]
        public async Task<IActionResult> Put(int id, Modelo modelo)
        {
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var modeloExiste = await _context.Modelo.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);
            
            if (modeloExiste is null)
            {
                return NotFound("Modelo não encontrado");
            }

            _context.Modelo.Remove(modeloExiste);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
