using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Modelo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo identificação da coleção é obrigatório")]
        public Colecao ColecaoId { get; set; }
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo tipo é obrigatório")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Campo layout da coleção é obrigatório")]
        public string Layout { get; set; }
    }
}
