using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Usuario : Pessoa
    {
        [Key]
        public int Identificador { get; set; }


        [Required(ErrorMessage = "Campo e-mail de nascimento é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido")]
        [MaxLength(200, ErrorMessage = "Campo e-mail pode ter no máximo 200 caracteres")]
        public string Email { get; set; }


        [MaxLength(100, ErrorMessage = "Campo tipo pode ter no máximo 100 caracteres")]
        public string Tipo { get; set; }


        [MaxLength(100, ErrorMessage = "Campo status pode ter no máximo 100 caracteres")]
        public string Status { get; set; }



    }
}
