namespace Books_Business_Tests.Modules.Genders.Entity
{
    [Collection(nameof(GenderCollection))]
    public class GenderEntityInvalidTests
    {
        private readonly GenderTestsFixture _genderTestsFixture;

        public GenderEntityInvalidTests(GenderTestsFixture genderTestsFixture)
        {
            _genderTestsFixture = genderTestsFixture;
        }

        [Fact(DisplayName = "New Empty Gender Must Be Invalid")]
        [Trait("Category", "Gender Entity Invalid Tests")]
        public void Gender_CreateEmpty_MustBeInvalid()
        {
            //Arrange
            var gender = _genderTestsFixture.GenerateEmptyGender();

            //Act
            var result = gender.IsValid();

            //Assert
            Assert.False(result);
            Assert.NotEmpty(gender.ValidationResult.Errors);
        }

        [Fact(DisplayName = "New Incomplete Gender Must Be Invalid")]
        [Trait("Category", "Gender Entity Invalid Tests")]
        public void Gender_CreateIncomplete_MustBeInvalid()
        {
            //Arrange
            var gender = _genderTestsFixture.GenerateInvalidGender();

            //Act
            var result = gender.IsValid();

            //Assert
            Assert.False(result);
            Assert.NotEmpty(gender.ValidationResult.Errors);
        }
    }
}