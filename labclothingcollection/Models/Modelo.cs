using labclothingcollection.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace labclothingcollection.Models
{
    [Index(nameof(Nome), IsUnique = true)]
    public class Modelo
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Colecao")]
        public int ColecaoId { get; set; }
        
        public virtual Colecao Colecao { get; set; }


        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "Campo nome pode ter no máximo 200 caracteres")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Campo tipo é obrigatório")]
        public EnumTipoModelo Tipo { get; set; }


        [Required(ErrorMessage = "Campo layout da coleção é obrigatório")]
        public EnumLayout Layout { get; set; }
    }
}
