using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Colecao
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo responsável é obrigatório")]
        public Usuario UsuarioId { get; set; }
        public Modelo ModeloId { get; set; }
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo status é obrigatório")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Campo marca é obrigatório")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "Campo orçamento é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Orcamento { get; set; }
        [Required(ErrorMessage = "Campo ano de lançamento é obrigatório")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime AnoLancamento { get; set; }
        [Required(ErrorMessage = "Campo estação é obrigatório")]
        public string Estacao { get; set; }
    }
}
