using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using labclothingcollection.Context;
using labclothingcollection.Models;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using labclothingcollection.DTO.Response;
using labclothingcollection.Models.Enum;

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
        public async Task<IActionResult> Get([FromQuery] EnumStatus? status)
        {
            List<Colecao> colecoes = await _context.Colecao.Where(x => status != null ? x.Status == status : x.Status != null)
                    .ToListAsync();

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Colecao, ColecaoResponseDTO>());

            var mapper = configuration.CreateMapper();

            List<ColecaoResponseDTO> colecaoResponseDTO = mapper.Map<List<ColecaoResponseDTO>>(colecoes);
            
            return Ok(colecaoResponseDTO);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var colecao = await _context.Colecao.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Colecao, ColecaoResponseDTO>());

            var mapper = configuration.CreateMapper();

            ColecaoResponseDTO colecaoResponseDTO = mapper.Map<ColecaoResponseDTO>(colecao);

            if (colecaoResponseDTO is null)
            {
                return NotFound("Coleção não encontrado");
            }

            return Ok(colecaoResponseDTO);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Colecao colecao)
        {
            bool existeColecao = await _context.Colecao.AnyAsync(x => x.Id == id).ConfigureAwait(true);

            if(!existeColecao)
            {
                return NotFound("Coleção não encontrada");
            }

            _context.Entry(colecao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] EnumStatus status)
        {
            var colecao = await _context.Colecao.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);


            if (colecao is null)
            {
                return NotFound("Coleção não encontrado");
            }

            colecao.Status = status;

            await _context.SaveChangesAsync();

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

                var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Colecao, ColecaoResponseDTO>());

                var mapper = configuration.CreateMapper();

                ColecaoResponseDTO colecaoResponseDTO = mapper.Map<ColecaoResponseDTO>(colecao);

                return Ok(CreatedAtAction(nameof(Get), new { id = colecaoResponseDTO.Id }, colecaoResponseDTO));
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
