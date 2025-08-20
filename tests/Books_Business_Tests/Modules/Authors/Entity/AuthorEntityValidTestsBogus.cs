using Books_Business_Tests.Modules.Authors.Fixtures;

namespace Books_Business_Tests.Modules.Authors.Entity
{
    [Collection(nameof(AuthorBogusCollection))]
    public class AuthorEntityValidTestsBogus
    {
        private readonly AuthorTestsBogusFixture _authorTestsFixture;

        public AuthorEntityValidTestsBogus(AuthorTestsBogusFixture authorTestsFixture)
        {
            _authorTestsFixture = authorTestsFixture;
        }

        [Fact(DisplayName = "New Author Must Be Valid Bogus")]
        [Trait("Category", "Author Entity Valid Tests")]
        public void Author_Create_MustBeValid_Bogus()
        {
            //Arrange
            var author = _authorTestsFixture.GenerateValidAuthorWithBogus();

            //Act
            var result = author.IsValid();

            //Assert
            Assert.True(result);
            Assert.Empty(author.ValidationResult.Errors);
        }
    }
}