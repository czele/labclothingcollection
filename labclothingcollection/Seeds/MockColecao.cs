using labclothingcollection.Models;

namespace labclothingcollection.Seeds
{
    public static class MockColecao
    {
        public static IList<Colecao> colecao = new List<Colecao>()
        {
            new Colecao
            {    
                Id = 1,
                UsuarioIdentificador = 1,
                Nome = "Nome 1",
                Status = Models.Enum.EnumStatus.Ativo,
                Marca = "Marca 1",
                Orcamento = 1000,
                Estacao = Models.Enum.EnumEstacao.Primavera,
                AnoLancamento = new DateTime(1991,05,22)
            },
            new Colecao 
            {
                Id = 2,
                UsuarioIdentificador = 2,
                Nome = "Nome 2",
                Status = Models.Enum.EnumStatus.Ativo,
                Marca = "Marca 2",
                Orcamento = 1500,
                Estacao = Models.Enum.EnumEstacao.Verao,
                AnoLancamento = new DateTime(1991, 05, 22)
            },
            new Colecao
            {
                Id = 3,
                UsuarioIdentificador = 1,
                Nome = "Nome 3",
                Status = Models.Enum.EnumStatus.Inativo,
                Marca = "Marca 3",
                Orcamento = 2000,
                Estacao = Models.Enum.EnumEstacao.Outono,
                AnoLancamento = new DateTime(1993, 07, 05)
            },
            new Colecao
            {
                Id = 4,
                UsuarioIdentificador = 1,
                Nome = "Nome 4",
                Status = Models.Enum.EnumStatus.Ativo,
                Marca = "Marca 4",
                Orcamento = 100,
                Estacao = Models.Enum.EnumEstacao.Inverno,
                AnoLancamento = new DateTime(1985, 01, 17)
            },
            new Colecao
            {
                Id = 5,
                UsuarioIdentificador = 2,
                Nome = "Nome 5",
                Status = Models.Enum.EnumStatus.Inativo,
                Marca = "Marca 5",
                Orcamento = 3500,
                Estacao = Models.Enum.EnumEstacao.Outono,
                AnoLancamento = new DateTime(1970, 10, 05)
            }

        };
    }
}
