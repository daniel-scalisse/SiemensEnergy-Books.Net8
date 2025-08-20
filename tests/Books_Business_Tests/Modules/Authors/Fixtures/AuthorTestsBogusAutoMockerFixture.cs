using Bogus;
using Bogus.DataSets;
using Books_Business.Modules.Authors;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Books_Business_Tests.Modules.Authors.Fixtures
{
    [CollectionDefinition(nameof(AuthorBogusAutoMockerCollection))]
    public class AuthorBogusAutoMockerCollection : ICollectionFixture<AuthorTestsBogusAutoMockerFixture>
    {
    }

    public class AuthorTestsBogusAutoMockerFixture : IDisposable
    {
        public AutoMocker Mocker;
        public AuthorService AuthorService;

        public Author GenerateValidAuthor()
        {
            return GenerateAuthorsWithBogus(1).FirstOrDefault();
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

        public AuthorService GetAuthorService()
        {
            Mocker = new AutoMocker();
            //Precisa ser a classe e não a interface.
            AuthorService = Mocker.CreateInstance<AuthorService>();

            return AuthorService;
        }

        public void Dispose()
        {

        }
    }
}