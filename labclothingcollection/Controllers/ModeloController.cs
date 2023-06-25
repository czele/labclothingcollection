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

        /// <summary>
        /// Lista de modelos cadastrados
        /// <param name="status">Status do modelo(ativo ou inativo)</param>
        /// <returns>Retorna uma lista dos modelos cadastrados</returns>
        /// <response code="200">Sucesso no retorno da lista dos modelos cadastrados</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] EnumLayout? layout)
        {
            try
            {
                List<Modelo> modelos = await _context.Modelo.Where(x => layout != null ? x.Layout == layout : x.Layout != null).
                        ToListAsync();

                var configuration = new MapperConfiguration(cfg => cfg.
                CreateMap<Modelo, ModeloResponseDTO>());

                var mapper = configuration.CreateMapper();

                List<ModeloResponseDTO> modeloResponseDTO = mapper.
                    Map<List<ModeloResponseDTO>>(modelos);

                return Ok(modeloResponseDTO);
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }

        /// <summary>
        /// Modelo cadastrado através do id
        /// <param name="id">id do modelo</param>
        /// <returns>Retorna o modelo cadastrado</returns>
        /// <response code="200">Sucesso no retorno do modelo cadastrado</response>
        /// <response code="404">Modelo não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Modelo>> Get(int id)
        {
            try
            {
                var modelo = await _context.Modelo.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

                var configuration = new MapperConfiguration(cfg => cfg.
                CreateMap<Modelo, ModeloResponseDTO>());

                var mapper = configuration.CreateMapper();

                ModeloResponseDTO modeloResponseDTO = mapper.
                    Map<ModeloResponseDTO>(modelo);

                if (modeloResponseDTO is null)
                {
                    return NotFound("Modelo não encontrado");
                }

                return Ok(modeloResponseDTO);
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }

        /// <summary>
        /// Atualização de um modelo através do id
        /// <param name="id">id da coleção</param>
        /// <param name="modelo">Atributos do modelo</param>
        /// <returns>Modelo atualizado</returns>
        /// <response code="204">Atualização realizada com sucesso</response>
        /// <response code="404">Modelo não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("id")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] Modelo modelo)
        {
            try
            {
                bool existeUsuario = await _context.Modelo.
                    AnyAsync(x => x.Id == id).ConfigureAwait(true);

                if (!existeUsuario)
                {
                    return NotFound("Modelo não encontrado");
                }

                _context.Entry(modelo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }

        /// <summary>
        /// Cadastro de um novo modelo
        /// <param name="modelo">Atributos do modelo</param>
        /// <returns>Retorna o modelo cadastrado</returns>
        /// <response code="201">Modelo criado com sucesso</response>
        /// <response code="409">Nome do modelo já cadastrado</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]Modelo modelo)
        {
            try
            {
                var modeloExiste = await _context.Modelo.
                    FirstOrDefaultAsync(x => x.Nome == modelo.Nome).
                    ConfigureAwait(true);

                if (modeloExiste is null)
                {
                    _context.Modelo.Add(modelo);

                    await _context.SaveChangesAsync();

                    var configuration = new MapperConfiguration(cfg => cfg.
                    CreateMap<Modelo, ModeloResponseDTO>());

                    var mapper = configuration.CreateMapper();

                    ModeloResponseDTO modeloResponseDTO = mapper.
                        Map<ModeloResponseDTO>(modelo);

                    return CreatedAtAction(nameof(Get), 
                        new { id = modeloResponseDTO.Id }, modeloResponseDTO);
                }

                return Conflict("Modelo já cadastrado");
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }

        /// <summary>
        /// Exclusão de modelo através do id
        /// <param name="id">id do modelo</param>
        /// <returns>Exclusão da coleção</returns>
        /// <response code="204">Exclusão do modelo realizada com sucesso</response>
        /// <response code="404">Modelo não encontrad0</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var modeloExiste = await _context.Modelo.
                    FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

                if (modeloExiste is null)
                {
                    return NotFound("Modelo não encontrado");
                }

                _context.Modelo.Remove(modeloExiste);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }
    }
}
