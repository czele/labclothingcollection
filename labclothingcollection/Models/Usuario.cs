using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Usuario : Pessoa
    {
        [Required(ErrorMessage = "Campo e-mail de nascimento é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido")]
        public string email { get; set; }
        public string tipo { get; set; }
        public string status { get; set; }

    }
}
