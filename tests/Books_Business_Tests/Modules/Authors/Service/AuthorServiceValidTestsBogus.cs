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
    [Collection(nameof(AuthorBogusCollection))]
    public class AuthorServiceValidTestsBogus
    {
        private readonly AuthorTestsBogusFixture _fixture;

        public AuthorServiceValidTestsBogus(AuthorTestsBogusFixture authorTestsFixture)
        {
            _fixture = authorTestsFixture;
        }

        [Fact(DisplayName = "New Author Service Must Be Valid Bogus")]
        [Trait("Category", "Author Service Valid Tests Bogus")]
        public async Task AuthorService_Create_MustBeValid()
        {
            //Arrange
            var author = _fixture.GenerateValidAuthorWithBogus();
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

        [Fact(DisplayName = "Get Authors Service Must Return Records Bogus")]
        [Trait("Category", "Author Service Valid Tests Bogus")]
        public async Task AuthorService_GetAuthor_MustReturnRecords()
        {
            //Arrange
            var authorRepo = new Mock<IAuthorRepository>();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();

            authorRepo.Setup(a => a.GetAll())
                .Returns(Task.FromResult(_fixture.GenerateAuthorsWithBogus(5)));

            var authorService = new AuthorService(authorRepo.Object, bookRepository.Object, notifier.Object);

            //Act
            var authors = await authorService.GetAll();

            //Assert
            authorRepo.Verify(r => r.GetAll(), Times.Once);
            Assert.True(authors.Any());
            Assert.False(authors.Count(a => a.Name != "") == 0);
        }
    }
}