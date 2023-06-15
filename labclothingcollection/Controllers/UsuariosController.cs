using labclothingcollection.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace labclothingcollection.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(/*[FromQuery] string status*/)
        {
            return Ok(MockUsuarios.usuario);
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Usuario usuario = MockUsuarios.usuario.FirstOrDefault(usuario => usuario.Identificador == id); 
            
            if(usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            MockUsuarios.usuario.Add(usuario);

            return CreatedAtAction(nameof(Get), new { id = usuario.Identificador }, usuario);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Usuario usuario)
        {
            Usuario usuarioLista = MockUsuarios.usuario.FirstOrDefault(usuario => usuario.Id == id);

            if (usuarioLista == null)
            {
                return NotFound();
            }

            var index = MockUsuarios.usuario.IndexOf(usuarioLista);

            if (index != -1)
            {
                MockUsuarios.usuario[index] = usuarioLista;
                return NoContent();
            }

            return Ok(usuario);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}/status")]
        public IActionResult PutStatus([FromRoute] int id, [FromBody] Status status)
        {
            Usuario usuarioLista = MockUsuarios.usuario.FirstOrDefault(usuario => usuario.Id == id);

            if (usuarioLista == null)
            {
                return NotFound();
            }

            var index = MockUsuarios.usuario.IndexOf(usuarioLista);

            if (index != -1)
            {
                MockUsuarios.usuario[index] = usuarioLista;
                return NoContent();
            }

            return Ok(status);

        }

    }
}
