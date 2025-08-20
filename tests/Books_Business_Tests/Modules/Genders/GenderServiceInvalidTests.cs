using Books_Business.Core.Notifications;
using Books_Business.Modules.Books;
using Books_Business.Modules.Genders;
using Moq;
using System.Threading.Tasks;

namespace Books_Business_Tests.Modules.Genders.Service
{
    [Collection(nameof(GenderCollection))]
    public class GenderServiceInvalidTests
    {
        private readonly GenderTestsFixture _fixture;

        public GenderServiceInvalidTests(GenderTestsFixture genderTestsFixture)
        {
            _fixture = genderTestsFixture;
        }

        [Fact(DisplayName = "New Incomplete Gender Service Must Be Invalid")]
        [Trait("Category", "Gender Service Invalid Tests")]
        public async Task GenderService_CreateIncomplete_MustBeInvalid()
        {
            //Arrange
            var gender = _fixture.GenerateInvalidGender();
            var genderRepository = new Mock<IGenderRepository>();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();
            var genderService = new GenderService(genderRepository.Object, bookRepository.Object, notifier.Object);

            //Act
            await genderService.Add(gender);

            Assert.False(gender.IsValid());
            genderRepository.Verify(r => r.Add(gender), Times.Never);
        }
    }
}