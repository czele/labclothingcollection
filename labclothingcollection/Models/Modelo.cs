using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Modelo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo identificação da coleção é obrigatório")]
        public Colecao ColecaoId { get; set; }


        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "Campo nome pode ter no máximo 200 caracteres")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Campo tipo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Campo tipo pode ter no máximo 100 caracteres")]
        public string Tipo { get; set; }


        [Required(ErrorMessage = "Campo layout da coleção é obrigatório")]
        [MaxLength(100, ErrorMessage = "Campo layout pode ter no máximo 100 caracteres")]
        public string Layout { get; set; }
    }
}
