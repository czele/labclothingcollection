using labclothingcollection.Models;
using labclothingcollection.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.DTO.Response
{
    public class UsuarioAtualizacaoResponseDTO
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime DataNascimento { get; set; }

        public string Telefone { get; set; }
        public EnumTipoUsuario Tipo { get; set; }

        public static implicit operator UsuarioAtualizacaoResponseDTO(Usuario usuario)
        {
            UsuarioAtualizacaoResponseDTO usuarioAtDTO = new UsuarioAtualizacaoResponseDTO
            {
                Nome = usuario.Nome,
                Genero = usuario.Genero,
                Telefone = usuario.Telefone,
                DataNascimento = usuario.DataNascimento,
                Tipo = usuario.Tipo,
            };

            return usuarioAtDTO;
        }
    }
}
