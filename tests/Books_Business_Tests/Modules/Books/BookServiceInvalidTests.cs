using Books_Business.Core.Notifications;
using Books_Business.Modules.Books;
using Moq;
using System.Threading.Tasks;

namespace Books_Business_Tests.Modules.Books.Service
{
    [Collection(nameof(BookCollection))]
    public class BookServiceInvalidTests
    {
        private readonly BookTestsFixture _fixture;

        public BookServiceInvalidTests(BookTestsFixture bookTestsFixture)
        {
            _fixture = bookTestsFixture;
        }

        [Fact(DisplayName = "New Incomplete Book Service Must Be Invalid")]
        [Trait("Category", "Book Service Invalid Tests")]
        public async Task BookService_CreateIncomplete_MustBeInvalid()
        {
            //Arrange
            var book = _fixture.GenerateInvalidBook();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();
            var bookService = new BookService(bookRepository.Object, notifier.Object);

            //Act
            await bookService.Add(book);

            Assert.False(book.IsValid());
            bookRepository.Verify(r => r.Add(book), Times.Never);
        }
    }
}