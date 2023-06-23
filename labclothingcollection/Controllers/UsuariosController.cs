using AutoMapper;
using labclothingcollection.Context;
using labclothingcollection.DTO.Response;
using labclothingcollection.Models;
using labclothingcollection.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;


namespace labclothingcollection.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly LabClothingCollectionContext _context;
        
        public UsuariosController(LabClothingCollectionContext labClothingCollectionContext)
        {
            _context = labClothingCollectionContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] EnumStatus? status)
        {
            try
            {
                List<Usuario> usuarios = await _context.Usuario.
                    Where(x => status != null ? x.Status == status : x.Status != null)
                    .ToListAsync();

                var configuration = new MapperConfiguration
                    (cfg => cfg.CreateMap<Usuario, UsuarioResponseDTO>());

                var mapper = configuration.CreateMapper();

                List<UsuarioResponseDTO> usuarioResponseDTO = mapper.
                    Map<List<UsuarioResponseDTO>>(usuarios);

                return Ok(usuarioResponseDTO);
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }

        }
    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetId(int id)
        {
            try
            {
                var usuario = await _context.Usuario.
                    FirstOrDefaultAsync(x => x.Identificador == id).
                    ConfigureAwait(true);

                var configuration = new MapperConfiguration(cfg => cfg.
                CreateMap<Usuario, UsuarioResponseDTO>());

                var mapper = configuration.CreateMapper();

                UsuarioResponseDTO usuarioResponseDTO = mapper.
                    Map<UsuarioResponseDTO>(usuario);

                if (usuarioResponseDTO is null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(usuarioResponseDTO);
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }

        }
       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                var UsuarioExiste = await _context.Usuario.
                    FirstOrDefaultAsync(x => x.Cpf == usuario.Cpf).
                    ConfigureAwait(true);

                if (UsuarioExiste is null)
                {
                    _context.Usuario.Add(usuario);

                    await _context.SaveChangesAsync();

                    var configuration = new MapperConfiguration(cfg => cfg.
                    CreateMap<Usuario, UsuarioResponseDTO>());

                    var mapper = configuration.CreateMapper();

                    UsuarioResponseDTO usuarioResponseDTO = mapper.
                        Map<UsuarioResponseDTO>(usuario);

                    return CreatedAtAction(nameof(Get), new { id = 
                        usuarioResponseDTO.Identificador }, usuarioResponseDTO);
                }

                return Conflict("Usuário já cadastrado");
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UsuarioAtualizacaoResponseDTO usuarioAtDTO)
        {
            try
            {
                bool existeUsuario = await _context.Usuario.
                    AnyAsync(x => x.Identificador == id).ConfigureAwait(true);

                if (!existeUsuario)
                {
                    return NotFound("Usuário não encontrado");
                }

                var usuario = await _context.Usuario.FindAsync(id);

                usuario.Nome = usuarioAtDTO.Nome;
                usuario.Genero = usuarioAtDTO.Genero;
                usuario.DataNascimento = usuarioAtDTO.DataNascimento;
                usuario.Telefone = usuarioAtDTO.Telefone;
                usuario.Tipo = usuarioAtDTO.Tipo;

                _context.Entry(usuario).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                var configuration = new MapperConfiguration(cfg => cfg.
                CreateMap<Usuario, UsuarioResponseDTO>());

                var mapper = configuration.CreateMapper();

                UsuarioResponseDTO usuarioResponseDTO = mapper.
                    Map<UsuarioResponseDTO>(usuario);

                return Ok(usuarioResponseDTO);
            }
            catch
            {
                return StatusCode(500, "O servidor achou um erro não esperado.");
            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] EnumStatus status)
        {
            try
            {
                var usuario = await _context.Usuario.
                    FirstOrDefaultAsync(x => x.Identificador == id).
                    ConfigureAwait(true);

                if (usuario is null)
                {
                    return NotFound("Usuário não encontrado");
                }

                usuario.Status = status;

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
