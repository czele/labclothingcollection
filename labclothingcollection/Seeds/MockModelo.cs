using labclothingcollection.Models;
using System.Diagnostics.CodeAnalysis;

namespace labclothingcollection.Seeds
{
    [ExcludeFromCodeCoverage]
    public static class MockModelo
    {
        public static IList<Modelo> modelo { get; set; } = new List<Modelo>()
        {
            new Modelo
            {
                Id = 1,
                ColecaoId = 1,
                Nome = "Nome 1",
                Tipo = Models.Enum.EnumTipoModelo.Bolsa,
                Layout = Models.Enum.EnumLayout.Bordado
            },

            new Modelo
            {
                Id = 2,
                ColecaoId = 2,
                Nome = "Nome 2",
                Tipo = Models.Enum.EnumTipoModelo.Calca,
                Layout = Models.Enum.EnumLayout.Liso
            },
            new Modelo
            {
                Id = 3,
                ColecaoId = 1,
                Nome = "Nome 3",
                Tipo = Models.Enum.EnumTipoModelo.Camisa,
                Layout = Models.Enum.EnumLayout.Estampa
            },
            new Modelo
            {
                Id = 4,
                ColecaoId = 1,
                Nome = "Nome 4",
                Tipo = Models.Enum.EnumTipoModelo.Calcados,
                Layout = Models.Enum.EnumLayout.Bordado
            },
            new Modelo
            {
                Id = 5,
                ColecaoId = 1,
                Nome = "Nome 5",
                Tipo = Models.Enum.EnumTipoModelo.Bermuda,
                Layout = Models.Enum.EnumLayout.Liso
            },
            new Modelo
            {
                Id = 6,
                ColecaoId = 2,
                Nome = "Nome 6",
                Tipo = Models.Enum.EnumTipoModelo.Bone,
                Layout = Models.Enum.EnumLayout.Estampa
            }

        };
    }
}
