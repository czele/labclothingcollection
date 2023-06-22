using labclothingcollection.Models;
using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.DTO.Response
{
    public class UsuarioResponseDTO
    {
        public int Identificador { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }

        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime DataNascimento { get; set; }


        public static implicit operator UsuarioResponseDTO(Usuario usuario)
        {
            UsuarioResponseDTO usuarioDTO = new UsuarioResponseDTO
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Genero = usuario.Genero,
                Cpf = usuario.Cpf,
                Telefone = usuario.Telefone,
                DataNascimento = usuario.DataNascimento,
                Status = usuario.Status.ToString(),
                Tipo = usuario.Tipo.ToString(),
            };

            return usuarioDTO;

        }

    }


}
