using Books_Business.Modules.Authors;
using System;
using System.Collections.Generic;

namespace Books_Business_Tests.Modules.Authors.Fixtures
{
    [CollectionDefinition(nameof(AuthorCollection))]
    public class AuthorCollection : ICollectionFixture<AuthorTestsFixture>
    {
    }

    //Usada para aproveitar mudança de estado do objeto.
    public class AuthorTestsFixture : IDisposable
    {
        public Author GenerateValidAuthor()
        {
            return new Author(1, "Julio Battisti");
        }

        public Author GenerateInvalidAuthor()
        {
            return new Author(0, "A");
        }

        public Author GenerateEmptyAuthor()
        {
            return new Author();
        }

        public IEnumerable<Author> GenerateAuthors()
        {
            var list = new List<Author>();
            list.Add(new Author(1, "Julio Battisti"));
            list.Add(new Author(2, "Daniel"));

            return list;
        }

        public void Dispose()
        {
        }
    }
}