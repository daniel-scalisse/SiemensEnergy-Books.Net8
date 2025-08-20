namespace Books_Business_Tests.Modules.Genders.Entity
{
    [Collection(nameof(GenderCollection))]
    public class GenderEntityValidTests
    {
        private readonly GenderTestsFixture _genderTestsFixture;

        public GenderEntityValidTests(GenderTestsFixture genderTestsFixture)
        {
            _genderTestsFixture = genderTestsFixture;
        }

        [Fact(DisplayName = "New Gender Must Be Valid")]
        [Trait("Category", "Gender Entity Valid Tests")]
        public void Gender_Create_MustBeValid()
        {
            //Arrange
            var gender = _genderTestsFixture.GenerateValidGender();

            //Act
            var result = gender.IsValid();

            //Assert
            Assert.True(result);
            Assert.Empty(gender.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Gender Must Inherit from Entity")]
        [Trait("Category", "Gender Entity Valid Tests")]
        public void Gender_New_MustReturnDerivedTypeEntity()
        {
            //Arrange
            var gender = _genderTestsFixture.GenerateEmptyGender();

            //Assert
            Assert.IsAssignableFrom<Books_Business.Core.Models.Entity>(gender);
        }
    }
}