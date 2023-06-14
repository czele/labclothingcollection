using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Usuario : Pessoa
    {
        [Required(ErrorMessage = "Campo e-mail de nascimento é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }

    }
}
