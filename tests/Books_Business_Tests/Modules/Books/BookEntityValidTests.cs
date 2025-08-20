namespace Books_Business_Tests.Modules.Books
{
    [Collection(nameof(BookCollection))]
    public class BookEntityValidTests
    {
        private readonly BookTestsFixture _bookTestsFixture;

        public BookEntityValidTests(BookTestsFixture bookTestsFixture)
        {
            _bookTestsFixture = bookTestsFixture;
        }

        [Fact(DisplayName = "New Book Must Be Valid")]
        [Trait("Category", "Book Entity Valid Tests")]
        public void Book_Create_MustBeValid()
        {
            //Arrange
            var book = _bookTestsFixture.GenerateValidBook();

            //Act
            var result = book.IsValid();

            //Assert
            Assert.True(result);
            Assert.Empty(book.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Book Must Inherit from Entity")]
        [Trait("Category", "Book Entity Valid Tests")]
        public void Book_New_MustReturnDerivedTypeEntity()
        {
            //Arrange
            var book = _bookTestsFixture.GenerateEmptyBook();

            //Assert
            Assert.IsAssignableFrom<Books_Business.Core.Models.Entity>(book);
        }
    }
}