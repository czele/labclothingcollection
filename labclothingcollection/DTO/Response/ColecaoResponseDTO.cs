using labclothingcollection.Models;
using labclothingcollection.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace labclothingcollection.DTO.Response
{
    public class ColecaoResponseDTO
    {
        public int Id { get; set; }
        public int UsuarioIdentificador { get; set; } //É UMA FOREING KEY
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
                UsuarioIdentificador = colecao.UsuarioIdentificador,
                Nome = colecao.Nome,
                Status = colecao.Status.ToString(),
                Marca = colecao.Marca,
                Orcamento = colecao.Orcamento,
                Estacao = colecao.Estacao.ToString(),
                AnoLancamento = colecao.AnoLancamento
            }
            return colecaoResponseDTO;
        }
    }
}
