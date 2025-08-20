using Books_Business.Modules.Authors;
using Books_Business_Tests.Modules.Authors.Fixtures;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Business_Tests.Modules.Authors.Service
{
    [Collection(nameof(AuthorBogusAutoMockerCollection))]
    public class AuthorServiceValidTestsBogusAutoMocker
    {
        private readonly AuthorTestsBogusAutoMockerFixture _fixture;
        private readonly AuthorService _authorService;

        public AuthorServiceValidTestsBogusAutoMocker(AuthorTestsBogusAutoMockerFixture authorTestsFixture)
        {
            _fixture = authorTestsFixture;
            _authorService = _fixture.GetAuthorService();
        }

        [Fact(DisplayName = "New Author Service Must Be Valid Bogus Auto Mocker")]
        [Trait("Category", "Author Service Valid Tests Bogus Auto Mocker")]
        public async Task AuthorService_Create_MustBeValid()
        {
            //Arrange
            var author = _fixture.GenerateValidAuthor();

            //Act
            await _authorService.Add(author);

            //Assert
            Assert.True(author.IsValid());
            _fixture.Mocker.GetMock<IAuthorRepository>().Verify(r => r.Add(author), Times.Once);
        }

        [Fact(DisplayName = "Get Authors Service Must Return Records Bogus Auto Mocker")]
        [Trait("Category", "Author Service Valid Tests Bogus Auto Mocker")]
        public async Task AuthorService_GetAuthor_MustReturnRecords()
        {
            //Arrange

            _fixture.Mocker.GetMock<IAuthorRepository>().Setup(a => a.GetAll())
                .Returns(Task.FromResult(_fixture.GenerateAuthorsWithBogus(5)));

            //Act
            var authors = await _authorService.GetAll();

            //Assert
            _fixture.Mocker.GetMock<IAuthorRepository>().Verify(r => r.GetAll(), Times.Once);
            Assert.True(authors.Any());
            Assert.False(authors.Count(a => a.Name != "") == 0);
        }
    }
}