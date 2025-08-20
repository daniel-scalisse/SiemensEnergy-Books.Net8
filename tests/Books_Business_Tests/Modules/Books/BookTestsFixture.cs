using Books_Business.Modules.Books;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Books_Business_Tests.Modules.Books
{
    [CollectionDefinition(nameof(BookCollection))]
    public class BookCollection : ICollectionFixture<BookTestsFixture>
    {
    }

    public class BookTestsFixture : IDisposable
    {
        public Book GenerateValidBook()
        {
            return new Book(1, 1, 1, "Sql Server 2022", "Básico", 2022, 1, 255, "1234567890", "B1234567890", null, null, false, null);
        }

        public Book GenerateInvalidBook()
        {
            return new Book(0, 0, 0, "", "", 0, 0, 0, "", "", null, null, false, null);
        }

        public Book GenerateEmptyBook()
        {
            return new Book();
        }

        public IEnumerable<Book> GenerateBooks()
        {
            var list = new List<Book>();
            list.Add(new Book(1, 1, 1, "Sql Server 2022", "Básico", 2022, 1, 255, "1234567890", "B1234567890", null, null, true, null));
            list.Add(new Book(2, 1, 1, "ASP.Net 8", "Avançado", 2023, 1, 255, "2345678901", "B2345678901", 100.55m, DateTime.Parse("2025-05-05", CultureInfo.CreateSpecificCulture("en-US")), false, "Troca de livros"));
            list.Add(new Book(3, 1, 1, "Sql Server 2022", "Básico", 2022, 1, 255, "3456789012", "B3456789012", null, null, false, null));

            return list;
        }

        public void Dispose()
        {
        }
    }
}