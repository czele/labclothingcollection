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

        /// <summary>
        /// Lista de coleções cadastradas
        /// <param name="status">Status da coleção(ativo ou inativo)</param>
        /// <returns>Retorna uma lista das coleções cadastradas</returns>
        /// <response code="200">Sucesso no retorno da lista de coleções cadastradas</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] EnumStatus? status)
        {
            try
            {
                List<Colecao> colecoes = await _context.Colecao.
                    Where(x => status != null ? x.Status == status : x.Status != null)
                        .ToListAsync();

                var configuration = new MapperConfiguration(cfg => cfg.
                CreateMap<Colecao, ColecaoResponseDTO>());

                var mapper = configuration.CreateMapper();

                List<ColecaoResponseDTO> colecaoResponseDTO = mapper.
                    Map<List<ColecaoResponseDTO>>(colecoes);

                return Ok(colecaoResponseDTO);
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
            
        }

        /// <summary>
        /// Coleção cadastrada através do id
        /// <param name="id">id da coleção</param>
        /// <returns>Retorna a coleção cadastrada</returns>
        /// <response code="200">Sucesso no retorno da coleção cadastrada</response>
        /// <response code="404">Coleção não encontrada</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var colecao = await _context.Colecao.
                    FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

                var configuration = new MapperConfiguration(cfg => cfg.
                CreateMap<Colecao, ColecaoResponseDTO>());

                var mapper = configuration.CreateMapper();

                ColecaoResponseDTO colecaoResponseDTO = mapper.
                    Map<ColecaoResponseDTO>(colecao);

                if (colecaoResponseDTO is null)
                {
                    return NotFound("Coleção não encontrado");
                }

                return Ok(colecaoResponseDTO);
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }

        /// <summary>
        /// Atualização de uma coleção através do id
        /// <param name="id">id da coleção</param>
        /// <param name="colecao">Atributos da coleção</param>
        /// <returns>Coleção atualizada</returns>
        /// <response code="204">Atualização realizada com sucesso</response>
        /// <response code="404">Coleção não encontrada</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Colecao colecao)
        {
            try
            {
                bool existeColecao = await _context.Colecao.
                    AnyAsync(x => x.Id == id).ConfigureAwait(true);

                if (!existeColecao)
                {
                    return NotFound("Coleção não encontrada");
                }

                _context.Entry(colecao).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }

        /// <summary>
        /// Atualização do status de uma coleção(ativo ou inativo) através do id
        /// <param name="id">id da coleção</param>
        /// <param name="status">Novo status da coleção</param>
        /// <returns>Retorna o status da coleção atualizada</returns>
        /// <response code="204">Atualização de status realizada com sucesso</response>
        /// <response code="404">Coleção não encontrada</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] EnumStatus status)
        {
            try
            {
                var colecao = await _context.Colecao.
                    FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);


                if (colecao is null)
                {
                    return NotFound("Coleção não encontrado");
                }

                colecao.Status = status;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }

        /// <summary>
        /// Cadastro de uma nova coleção
        /// <param name="colecao">Atributos da coleção</param>
        /// <returns>Retorna a coleção cadastrada</returns>
        /// <response code="201">Cadastro criado com sucesso</response>
        /// <response code="409">Nome de coleção já cadastrado</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]Colecao colecao)
        {
            try
            {
                var colecaoExiste = await _context.Colecao.
                    FirstOrDefaultAsync(x => x.Nome == colecao.Nome).
                    ConfigureAwait(true);

                if (colecaoExiste is null)
                {
                    _context.Colecao.Add(colecao);

                    await _context.SaveChangesAsync();

                    var configuration = new MapperConfiguration(cfg => cfg.
                    CreateMap<Colecao, ColecaoResponseDTO>());

                    var mapper = configuration.CreateMapper();

                    ColecaoResponseDTO colecaoResponseDTO = mapper.
                        Map<ColecaoResponseDTO>(colecao);

                    return Ok(CreatedAtAction(nameof(Get), 
                        new { id = colecaoResponseDTO.Id }, colecaoResponseDTO));
                }

                return Conflict("Coleção já cadastrada");
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }
        }

        /// <summary>
        /// Exclusão de coleção através do id
        /// <param name="id">id da coleção</param>
        /// <returns>Exclusão da coleção</returns>
        /// <response code="204">Exclusão da coleção realizada com sucesso</response>
        /// <response code="404">Coleção não encontrada</response>
        /// <response code="403">Não é possível a exclusão de uma coleção ativa ou que possua modelos vinculados</response>
        /// <response code="500">Erro interno do servidor</response>
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var colecaoExiste = await _context.Colecao.
                    FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(true);

                if (colecaoExiste is null)
                {
                    return NotFound("Coleção não encontrada");
                }

                if (colecaoExiste.Status == EnumStatus.Ativo)
                {
                    return StatusCode(403, "Não é possível deletar uma coleção ativa");
                }

                bool existeModelo = await _context.Modelo.
                    AnyAsync(x => x.ColecaoId == colecaoExiste.Id);
                if (existeModelo)
                {
                    return StatusCode(403, "Não é possível deletar uma coleção que possua modelos atrelados.");
                }

                _context.Colecao.Remove(colecaoExiste);
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
