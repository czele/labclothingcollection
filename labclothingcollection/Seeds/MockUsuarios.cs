using labclothingcollection.Models;
using System.Diagnostics.CodeAnalysis;

namespace labclothingcollection.Seeds
{
    [ExcludeFromCodeCoverage]
    public static class MockUsuarios
    {
        public static IList<Usuario> usuario { get; set; } = new List<Usuario>()
        {
            new Usuario
            {
                Identificador = 1,
                Email = "teste@teste.com",
                Tipo = Models.Enum.EnumTipoUsuario.Criador,
                Status = Models.Enum.EnumStatus.Ativo,
                Nome = "Lucas Silva",
                Genero = "masculino",
                DataNascimento = new DateTime(1991,05,22),
                Cpf = "123.456.789-00",
                Telefone = "9512328"
            },
            new Usuario
            {
                Identificador = 2,
                Email = "teste2@example.com",
                Tipo = Models.Enum.EnumTipoUsuario.Administrador,
                Status = Models.Enum.EnumStatus.Inativo,
                Nome = "Camila Z",
                Genero = "feminino",
                DataNascimento = new DateTime(1930,06,21),
                Cpf = "987.654.321-00",
                Telefone = "987654321"
            },
            new Usuario
            {
                Identificador = 3,
                Email = "teste3@dominio.com",
                Tipo = Models.Enum.EnumTipoUsuario.Gerente,
                Status = Models.Enum.EnumStatus.Inativo,
                Nome = "Maria das Dores",
                Genero = "feminino",
                DataNascimento = new DateTime(1998,9,5),
                Cpf = "456.789.123-00",
                Telefone = "456789123"
            },
            new Usuario {
                Identificador = 4,
                Email = "teste4@example.com",
                Tipo = Models.Enum.EnumTipoUsuario.Outro,
                Status = Models.Enum.EnumStatus.Ativo,
                Nome = "Ana Rodrigues",
                Genero = "feminino",
                DataNascimento = new DateTime(1987,3,12),
                Cpf = "789.123.456-00",
                Telefone = "321654987"
            },
            new Usuario {
                Identificador = 5,
                Email = "teste5@example.com",
                Tipo = Models.Enum.EnumTipoUsuario.Outro,
                Status = Models.Enum.EnumStatus.Ativo,
                Nome = "Luiz Ferreira",
                Genero = "Masculino",
                DataNascimento = new DateTime(1982,3,12),
                Cpf = "321.654.987-00",
                Telefone = "321654987"
            }

        };
    }
}
