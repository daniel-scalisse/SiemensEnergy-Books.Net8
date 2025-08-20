using Books_Business.Core.Notifications;
using Books_Business.Modules.Authors;
using Books_Business.Modules.Books;
using Books_Business_Tests.Modules.Authors.Fixtures;
using Moq;
using System.Threading.Tasks;

namespace Books_Business_Tests.Modules.Authors.Service
{
    [Collection(nameof(AuthorCollection))]
    public class AuthorServiceInvalidTests
    {
        private readonly AuthorTestsFixture _fixture;

        public AuthorServiceInvalidTests(AuthorTestsFixture authorTestsFixture)
        {
            _fixture = authorTestsFixture;
        }

        [Fact(DisplayName = "New Incomplete Author Service Must Be Invalid")]
        [Trait("Category", "Author Service Invalid Tests")]
        public async Task AuthorService_CreateIncomplete_MustBeInvalid()
        {
            //Arrange
            var author = _fixture.GenerateInvalidAuthor();
            var authorRepository = new Mock<IAuthorRepository>();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();
            var authorService = new AuthorService(authorRepository.Object, bookRepository.Object, notifier.Object);

            //Act
            await authorService.Add(author);

            Assert.False(author.IsValid());
            authorRepository.Verify(r => r.Add(author), Times.Never);
        }
    }
}