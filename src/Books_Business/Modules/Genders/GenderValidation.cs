using FluentValidation;

namespace Books_Business.Modules.Genders
{
    public class GenderValidation : AbstractValidator<Gender>
    {
        public GenderValidation()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("The {PropertyName} field is required.")
                .Length(GenderMaxLength.NameMin, GenderMaxLength.NameMax)
                .WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}