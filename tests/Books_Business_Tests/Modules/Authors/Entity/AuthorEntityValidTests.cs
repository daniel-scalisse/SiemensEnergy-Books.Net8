using Books_Business_Tests.Modules.Authors.Fixtures;

namespace Books_Business_Tests.Modules.Authors.Entity
{
    [Collection(nameof(AuthorCollection))]
    public class AuthorEntityValidTests
    {
        private readonly AuthorTestsFixture _authorTestsFixture;

        public AuthorEntityValidTests(AuthorTestsFixture authorTestsFixture)
        {
            _authorTestsFixture = authorTestsFixture;
        }

        [Fact(DisplayName = "New Author Must Be Valid")]
        [Trait("Category", "Author Entity Valid Tests")]
        public void Author_Create_MustBeValid()
        {
            //Arrange
            var author = _authorTestsFixture.GenerateValidAuthor();

            //Act
            var result = author.IsValid();

            //Assert
            Assert.True(result);
            Assert.Empty(author.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Author Must Inherit from Entity")]
        [Trait("Category", "Author Entity Valid Tests")]
        public void Author_New_MustInheritFromEntity()
        {
            //Arrange
            var author = _authorTestsFixture.GenerateEmptyAuthor();

            //Assert
            Assert.IsAssignableFrom<Books_Business.Core.Models.Entity>(author);
        }
    }
}