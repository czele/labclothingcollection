using AutoMapper;
using labclothingcollection.Context;
using labclothingcollection.DTO.Response;
using labclothingcollection.Models;
using labclothingcollection.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace labclothingcollection.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly LabClothingCollectionContext _context;
        >
        public UsuariosController(LabClothingCollectionContext labClothingCollectionContext)
        {
            _context = labClothingCollectionContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] EnumStatus? status)
        {
            List<Usuario> usuarios = await _context.Usuario.Where(x => status != null ? x.Status == status : x.Status != null)
                .ToListAsync();

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Usuario, UsuarioResponseDTO>());

            var mapper = configuration.CreateMapper();

            List<UsuarioResponseDTO> usuarioResponseDTO = mapper.Map<List<UsuarioResponseDTO>>(usuarios);

            return Ok(usuarioResponseDTO);

        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetId(int id)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Identificador == id).ConfigureAwait(true);

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Usuario, UsuarioResponseDTO>());

            var mapper = configuration.CreateMapper();

            UsuarioResponseDTO usuarioResponseDTO = mapper.Map<UsuarioResponseDTO>(usuario);

            if (usuarioResponseDTO is null)
            {
                return NotFound("Usuário não encontrado");
            }

            return Ok(usuarioResponseDTO);
        }

       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            var UsuarioExiste = await _context.Usuario.FirstOrDefaultAsync(x => x.Cpf == usuario.Cpf).ConfigureAwait(true);

            if (UsuarioExiste is null)
            {
                _context.Usuario.Add(usuario);

                await _context.SaveChangesAsync();

                var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Usuario, UsuarioResponseDTO>());

                var mapper = configuration.CreateMapper();

                UsuarioResponseDTO usuarioResponseDTO = mapper.Map<UsuarioResponseDTO>(usuario);

                return CreatedAtAction(nameof(Get), new { id = usuarioResponseDTO.Identificador }, usuarioResponseDTO);
            }

            return Conflict("Usuário já cadastrado");

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Usuario usuario)
        {
            bool existeUsuario = await _context.Usuario.AnyAsync(x => x.Identificador == id).ConfigureAwait(true);

            if (!existeUsuario)
            {
                return NotFound("Usuário não encontrado");
            }

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] EnumStatus status)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Identificador == id).ConfigureAwait(true);


            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }

            usuario.Status = status; 

            await _context.SaveChangesAsync(); 

            return NoContent();

        }

    }
}
