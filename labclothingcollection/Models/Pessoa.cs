using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }

        [Required(ErrorMessage = "Campo data de nascimento é obrigatório")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public string DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
    }
}
