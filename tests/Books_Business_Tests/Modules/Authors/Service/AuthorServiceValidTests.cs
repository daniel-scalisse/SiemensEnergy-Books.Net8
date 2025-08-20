using Books_Business.Core.Notifications;
using Books_Business.Modules.Authors;
using Books_Business.Modules.Books;
using Books_Business_Tests.Modules.Authors.Fixtures;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Business_Tests.Modules.Authors.Service
{
    [Collection(nameof(AuthorCollection))]
    public class AuthorServiceValidTests
    {
        private readonly AuthorTestsFixture _fixture;

        public AuthorServiceValidTests(AuthorTestsFixture authorTestsFixture)
        {
            _fixture = authorTestsFixture;
        }

        [Fact(DisplayName = "New Author Service Must Be Valid")]
        [Trait("Category", "Author Service Valid Tests")]
        public async Task AuthorService_Create_MustBeValid()
        {
            //Arrange
            var author = _fixture.GenerateValidAuthor();
            var authorRepository = new Mock<IAuthorRepository>();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();
            var authorService = new AuthorService(authorRepository.Object, bookRepository.Object, notifier.Object);

            //Act
            await authorService.Add(author);

            //Assert - O próprio Moq faz um assert interno.
            Assert.True(author.IsValid());//Não precisa porque o próprio Add já faz a validação.
            authorRepository.Verify(r => r.Add(author), Times.Once);
        }

        [Fact(DisplayName = "Get Authors Service Must Return Records")]
        [Trait("Category", "Author Service Valid Tests")]
        public async Task AuthorService_GetAuthors_MustReturnRecords()
        {
            //Arrange
            var authorRepository = new Mock<IAuthorRepository>();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();

            authorRepository.Setup(a => a.GetAll())
                .Returns(Task.FromResult(_fixture.GenerateAuthors()));

            var authorService = new AuthorService(authorRepository.Object, bookRepository.Object, notifier.Object);

            //Act
            var authors = await authorService.GetAll();

            //Assert
            authorRepository.Verify(r => r.GetAll(), Times.Once);
            Assert.True(authors.Any());
            Assert.False(authors.Count(a => a.Name != "") == 0);
        }
    }
}