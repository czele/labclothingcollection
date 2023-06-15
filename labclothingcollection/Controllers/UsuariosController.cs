using labclothingcollection.Context;
using labclothingcollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> Get([FromQuery] string? status)
        {
            if(status is null)
            {
                return Ok(await _context.Usuario.ToListAsync().ConfigureAwait(true));
            }

            List<Usuario> usuarios = await _context.Usuario.Where(x => x.Status == status).ToListAsync();

            return Ok(usuarios);

        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await _context.Usuario.
                FirstOrDefaultAsync(x => x.Identificador == id).ConfigureAwait(true); 
            
            if(usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }

            return Ok(usuario);
        }

       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            _context.Usuario.Add(usuario);

            await _context.SaveChangesAsync();   

            return CreatedAtAction(nameof(Get), new { id = usuario.Identificador }, usuario);
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

        /*[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutStatus([FromRoute] int id, [FromBody] Status status)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Identificador == id).ConfigureAwait(true);
                
       
            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }

            usuario.Status = status; // Atualiza o status do usuário

            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados

            return NoContent();

        }*/

    }
}
