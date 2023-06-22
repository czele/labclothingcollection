using labclothingcollection.Models;
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
        public string Tipo { get; set; }

        public static implicit operator UsuarioAtualizacaoResponseDTO(Usuario usuario)
        {
            UsuarioAtualizacaoResponseDTO usuarioAtDTO = new UsuarioAtualizacaoResponseDTO
            {
                Nome = usuario.Nome,
                Genero = usuario.Genero,
                Telefone = usuario.Telefone,
                DataNascimento = usuario.DataNascimento,
                Tipo = usuario.Tipo.ToString(),
            };

            return usuarioAtDTO;
        }
    }
}
