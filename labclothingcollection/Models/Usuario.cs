using labclothingcollection.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Usuario : Pessoa
    {

        [Required(ErrorMessage = "Campo e-mail de nascimento é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido")]
        [MaxLength(200, ErrorMessage = "Campo e-mail pode ter no máximo 200 caracteres")]
        public string Email { get; set; }

        [Required]
        public EnumTipoUsuario Tipo { get; set; }

        [Required]
        public EnumStatus Status { get; set; }

    }
}
