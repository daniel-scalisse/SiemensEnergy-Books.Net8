using Bogus;
using Bogus.DataSets;
using Books_Business.Modules.Authors;
using System;
using System.Collections.Generic;

namespace Books_Business_Tests.Modules.Authors.Fixtures
{
    [CollectionDefinition(nameof(AuthorBogusCollection))]
    public class AuthorBogusCollection : ICollectionFixture<AuthorTestsBogusFixture>
    {
    }

    public class AuthorTestsBogusFixture : IDisposable
    {
        /// <summary>
        /// Usa Bogus para dados aleatórios.
        /// Testes mais próximos da realidade.
        /// Não fica preso a dados fixos, e não precisa ficar alterando o fonte do teste.
        /// Para teste de integração não precisa ficar alterando toda vez, porque o dado já foi inserido.
        /// </summary>
        /// <returns></returns>
        public Author GenerateValidAuthorWithBogus()
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            //Se a classe não possui construtor.
            //var authorFaker = new Faker<Author>();
            //authorFaker.RuleFor(a => a.Name, (f, a) => f.Name.FullName());

            //Uso o construtor da classe.
            var author = new Faker<Author>("pt_BR")
                .CustomInstantiator(f => new Author
                (
                    1,
                    f.Name.FullName(gender)
                ));

            //Propriedade que contém os dados da pessoa:
            //author.FakerHub.Person.FullName

            return author;
        }

        public IEnumerable<Author> GenerateAuthorsWithBogus(int quantity)
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var authors = new Faker<Author>("pt_BR")
                .CustomInstantiator(f => new Author
                (
                    1,
                    f.Name.FullName(gender)
                ));

            return authors.Generate(quantity);
        }

        public void Dispose()
        {
        }
    }
}