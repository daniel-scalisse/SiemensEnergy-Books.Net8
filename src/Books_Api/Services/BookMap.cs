using Books_Api.ViewModels;
using Books_Business.Modules.Books;
using System.Collections.Generic;
using System.Linq;

namespace Books_Api.Services
{
    public class BookMap
    {
        public IEnumerable<BookView> FromBookToBookView(IEnumerable<Book> books)
        {
            return from b in books
                   select (new BookView
                   {
                       Id = b.Id,
                       GenderName = b.Gender.Name,
                       AuthorName = b.Author.Name,
                       Title = b.Title,
                       Subtitle = b.SubTitle,
                       Year = b.Year,
                       Edition = b.Edition,
                       PageQuantity = b.PageQuantity,
                       ISBN = b.ISBN,
                       Barcode = b.Barcode,
                       Value = b.Value,
                       PurchaseDate = b.PurchaseDate,
                       Dedication = b.Dedication,
                       Observation = b.Observation,
                       InclusionDate = b.InclusionDate
                   });
        }
    }
}