using labclothingcollection.Models;


namespace labclothingcollection.DTO.Response
{
    public class ModeloResponseDTO
    {
        public int Id { get; set; }
        public int ColecaoId { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Layout { get; set; }

        public static implicit operator ModeloResponseDTO(Modelo modelo)
        {
            ModeloResponseDTO modeloResponseDTO = new ModeloResponseDTO
            {
                Id = modelo.Id,
                ColecaoId = modelo.ColecaoId,
                Nome = modelo.Nome,
                Tipo = modelo.Tipo.ToString(),
                Layout = modelo.Layout.ToString()
            };
            return modeloResponseDTO;
        }
    }
}
