using Books_Business.Core.Notifications;
using Books_Business.Modules.Books;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Business_Tests.Modules.Books.Service
{
    [Collection(nameof(BookCollection))]
    public class BookServiceValidTests
    {
        private readonly BookTestsFixture _fixture;

        public BookServiceValidTests(BookTestsFixture bookTestsFixture)
        {
            _fixture = bookTestsFixture;
        }

        [Fact(DisplayName = "New Book Service Must Be Valid")]
        [Trait("Category", "Book Service Valid Tests")]
        public async Task BookService_Create_MustBeValid()
        {
            //Arrange
            var book = _fixture.GenerateValidBook();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();
            var bookService = new BookService(bookRepository.Object, notifier.Object);

            //Act
            await bookService.Add(book);

            //Assert
            Assert.True(book.IsValid());
            bookRepository.Verify(r => r.Add(book), Times.Once);
        }

        [Fact(DisplayName = "Get Books Service Must Return Records")]
        [Trait("Category", "Book Service Valid Tests")]
        public async Task BookService_GetBooks_MustReturnRecords()
        {
            //Arrange
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();

            bookRepository.Setup(a => a.GetAll())
                .Returns(Task.FromResult(_fixture.GenerateBooks()));

            var bookService = new BookService(bookRepository.Object, notifier.Object);

            //Act
            var books = await bookService.GetAll();

            //Assert
            bookRepository.Verify(r => r.GetAll(), Times.Once);
            Assert.True(books.Any());
            Assert.False(books.Count(a => a.Title != "") == 0);
        }
    }
}