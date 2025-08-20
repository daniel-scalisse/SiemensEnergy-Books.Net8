namespace Books_Business_Tests.Modules.Books.Entity
{
    [Collection(nameof(BookCollection))]
    public class BookEntityInvalidTests
    {
        private readonly BookTestsFixture _bookTestsFixture;

        public BookEntityInvalidTests(BookTestsFixture bookTestsFixture)
        {
            _bookTestsFixture = bookTestsFixture;
        }

        [Fact(DisplayName = "New Empty Book Must Be Invalid")]
        [Trait("Category", "Book Entity Invalid Tests")]
        public void Book_CreateEmpty_MustBeInvalid()
        {
            //Arrange
            var book = _bookTestsFixture.GenerateEmptyBook();

            //Act
            var result = book.IsValid();

            //Assert
            Assert.False(result);
            Assert.NotEmpty(book.ValidationResult.Errors);
        }

        [Fact(DisplayName = "New Incomplete Book Must Be Invalid")]
        [Trait("Category", "Book Entity Invalid Tests")]
        public void Book_CreateIncomplete_MustBeInvalid()
        {
            //Arrange
            var book = _bookTestsFixture.GenerateInvalidBook();

            //Act
            var result = book.IsValid();

            //Assert
            Assert.False(result);
            Assert.NotEmpty(book.ValidationResult.Errors);
        }
    }
}