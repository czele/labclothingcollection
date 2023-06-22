using labclothingcollection.Models;
using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.DTO.Response
{
    public class ColecaoResponseDTO
    {
        public int Id { get; set; }
        public Usuario? Usuario { get; set; } //É foreign key. Caso de errado substitui para public int UsuarioIdentificador
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Marca { get; set; }
        public int Orcamento { get; set; }
        public string Estacao { get; set; }

        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime AnoLancamento { get; set; }

        public static implicit operator ColecaoResponseDTO(Colecao colecao)
        {
            ColecaoResponseDTO colecaoResponseDTO = new ColecaoResponseDTO
            {
                Id = colecao.Id,
                Usuario = colecao.Usuario, //subistitui aqui se der errado aqui também
                Nome = colecao.Nome,
                Status = colecao.Status.ToString(),
                Marca = colecao.Marca,
                Orcamento = colecao.Orcamento,
                Estacao = colecao.Estacao.ToString(),
                AnoLancamento = colecao.AnoLancamento
            };
            return colecaoResponseDTO;
        }
    }
}
