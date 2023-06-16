using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace labclothingcollection.Models
{
    public class Colecao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo responsável é obrigatório")]
        [ForeignKey("Usuario")]
        public int UsuarioIdentificador { get; set; }
        public virtual Usuario Usuario { get; set; }


        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "Campo nome pode ter no máximo 200 caracteres")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Campo status é obrigatório")]
        [MaxLength(100, ErrorMessage = "Campo tipo pode ter no máximo 100 caracteres")]
        public string Status { get; set; }


        [Required(ErrorMessage = "Campo marca é obrigatório")]
        [MaxLength(100, ErrorMessage = "Campo marca pode ter no máximo 100 caracteres")]
        public string Marca { get; set; }


        [Required(ErrorMessage = "Campo orçamento é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int Orcamento { get; set; }


        [Required(ErrorMessage = "Campo ano de lançamento é obrigatório")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime AnoLancamento { get; set; }


        [Required(ErrorMessage = "Campo estação é obrigatório")]
        [MaxLength(100, ErrorMessage = "Campo estação pode ter no máximo 100 caracteres")]
        public string Estacao { get; set; }
    }
}
