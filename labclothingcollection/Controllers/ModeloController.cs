using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using labclothingcollection.Context;
using labclothingcollection.Models;
using Swashbuckle.AspNetCore.Annotations;
using labclothingcollection.Models.Enum;
using AutoMapper;
using labclothingcollection.DTO.Response;

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
        public async Task<IActionResult> Get([FromQuery] EnumLayout? layout)
        {
            List<Modelo> modelos = await _context.Modelo.Where(x => layout != null ? x.Layout == layout : x.Layout != null).
                    ToListAsync();

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Modelo, ModeloResponseDTO>());
               
            var mapper = configuration.CreateMapper();

            List<ModeloResponseDTO> modeloResponseDTO = mapper.Map<List<ModeloResponseDTO>>(modelos);
            
            return Ok(modeloResponseDTO);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Modelo>> Get(int id)
        {

            var modelo = await _context.Modelo.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Modelo, ModeloResponseDTO>());

            var mapper = configuration.CreateMapper();

            ModeloResponseDTO modeloResponseDTO = mapper.Map<ModeloResponseDTO>(modelo);
            
            if(modeloResponseDTO is null) 
            {
                return NotFound("Modelo não encontrado");
            }
            
            return Ok(modeloResponseDTO);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("id")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] Modelo modelo)
        {
            bool existeUsuario = await _context.Modelo.AnyAsync(x => x.Id == id).ConfigureAwait(true); 
            
            if(!existeUsuario)
            {
                return NotFound("Modelo não encontrado");
            }

            _context.Entry(modelo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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

                var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Modelo, ModeloResponseDTO>());

                var mapper = configuration.CreateMapper();

                ModeloResponseDTO modeloResponseDTO = mapper.Map<ModeloResponseDTO>(modelo);

                return CreatedAtAction(nameof(Get), new { id = modeloResponseDTO.Id }, modeloResponseDTO);
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
