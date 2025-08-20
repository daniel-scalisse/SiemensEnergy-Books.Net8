using Books_Business.Modules.Genders;
using System;
using System.Collections.Generic;

namespace Books_Business_Tests.Modules.Genders
{
    [CollectionDefinition(nameof(GenderCollection))]
    public class GenderCollection : ICollectionFixture<GenderTestsFixture>
    {
    }

    public class GenderTestsFixture : IDisposable
    {
        public Gender GenerateValidGender()
        {
            return new Gender(1, "Banco de Dados");
        }

        public Gender GenerateInvalidGender()
        {
            return new Gender(0, "");
        }

        public Gender GenerateEmptyGender()
        {
            return new Gender();
        }

        public IEnumerable<Gender> GenerateGenders()
        {
            var list = new List<Gender>();
            list.Add(new Gender(1, "Banco de Dados"));
            list.Add(new Gender(2, "Linguagem de Programação"));
            list.Add(new Gender(3, "Redes de Computadores"));
            list.Add(new Gender(4, "Sistemas Operacionais"));

            return list;
        }

        public void Dispose()
        {
        }
    }
}