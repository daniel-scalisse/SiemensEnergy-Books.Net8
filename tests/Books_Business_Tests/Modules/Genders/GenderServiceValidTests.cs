using Books_Business.Core.Notifications;
using Books_Business.Modules.Books;
using Books_Business.Modules.Genders;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Business_Tests.Modules.Genders.Service
{
    [Collection(nameof(GenderCollection))]
    public class GenderServiceValidTests
    {
        private readonly GenderTestsFixture _fixture;

        public GenderServiceValidTests(GenderTestsFixture genderTestsFixture)
        {
            _fixture = genderTestsFixture;
        }

        [Fact(DisplayName = "New Gender Service Must Be Valid")]
        [Trait("Category", "Gender Service Valid Tests")]
        public async Task GenderService_Create_MustBeValid()
        {
            //Arrange
            var gender = _fixture.GenerateValidGender();
            var genderRepository = new Mock<IGenderRepository>();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();
            var genderService = new GenderService(genderRepository.Object, bookRepository.Object, notifier.Object);

            //Act
            await genderService.Add(gender);

            //Assert
            Assert.True(gender.IsValid());
            genderRepository.Verify(r => r.Add(gender), Times.Once);
        }

        [Fact(DisplayName = "Get Genders Service Must Return Records")]
        [Trait("Category", "Gender Service Valid Tests")]
        public async Task GenderService_GetGenders_MustReturnRecords()
        {
            //Arrange
            var genderRepository = new Mock<IGenderRepository>();
            var bookRepository = new Mock<IBookRepository>();
            var notifier = new Mock<INotifier>();

            genderRepository.Setup(a => a.GetAll())
                .Returns(Task.FromResult(_fixture.GenerateGenders()));

            var genderService = new GenderService(genderRepository.Object, bookRepository.Object, notifier.Object);

            //Act
            var genders = await genderService.GetAll();

            //Assert
            genderRepository.Verify(r => r.GetAll(), Times.Once);
            Assert.True(genders.Any());
            Assert.False(genders.Count(a => a.Name != "") == 0);
        }
    }
}