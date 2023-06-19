using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    [Index(nameof(Cpf), IsUnique = true)]
    public class Pessoa
    {
        [Key]
        public int Identificador { get; set; }

        [MaxLength(200, ErrorMessage = "Campo nome pode ter no máximo 200 caracteres")]
        public string Nome { get; set; }


        [MaxLength(100, ErrorMessage = "Campo gênero pode ter no máximo 100 caracteres")]
        public string Genero { get; set; }


        [Required(ErrorMessage = "Campo data de nascimento é obrigatório")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime DataNascimento { get; set; }


        [MaxLength(100, ErrorMessage = "Campo CPF pode ter no máximo 100 caracteres")]
        public string Cpf { get; set; }


        [MaxLength(100, ErrorMessage = "Campo telefone pode ter no máximo 100 caracteres")]
        public string Telefone { get; set; }
    }
}
