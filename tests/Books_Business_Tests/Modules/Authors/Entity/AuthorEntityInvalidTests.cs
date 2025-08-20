using Books_Business_Tests.Modules.Authors.Fixtures;

namespace Books_Business_Tests.Modules.Authors.Entity
{
    [Collection(nameof(AuthorCollection))]
    public class AuthorEntityInvalidTests
    {
        private readonly AuthorTestsFixture _authorTestsFixture;

        public AuthorEntityInvalidTests(AuthorTestsFixture authorTestsFixture)
        {
            _authorTestsFixture = authorTestsFixture;
        }

        [Fact(DisplayName = "New Empty Author Must Be Invalid")]
        [Trait("Category", "Author Entity Invalid Tests")]
        public void Author_CreateEmpty_MustBeInvalid()
        {
            //Arrange
            var author = _authorTestsFixture.GenerateEmptyAuthor();

            //Act
            var result = author.IsValid();

            //Assert
            Assert.False(result);
            Assert.NotEmpty(author.ValidationResult.Errors);
        }

        [Fact(DisplayName = "New Incomplete Author Must Be Invalid")]
        [Trait("Category", "Author Entity Invalid Tests")]
        public void Author_CreateIncomplete_MustBeInvalid()
        {
            //Arrange
            var author = _authorTestsFixture.GenerateInvalidAuthor();

            //Act
            var result = author.IsValid();

            //Assert
            Assert.False(result);
            Assert.NotEmpty(author.ValidationResult.Errors);
        }
    }
}