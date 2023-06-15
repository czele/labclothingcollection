using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Status
    {
        [Required(ErrorMessage = "Campo status de nascimento é obrigatório")]
        [MaxLength(100, ErrorMessage = "Campo status pode ter no máximo 100 caracteres")]
        public string StatusUsuario { get; set; }
    }
}
