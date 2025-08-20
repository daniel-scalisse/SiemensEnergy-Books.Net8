using FluentValidation;

namespace Books_Business.Modules.Authors
{
    public class AuthorValidation : AbstractValidator<Author>
    {
        public AuthorValidation()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("The {PropertyName} field is required.")
                .Length(AuthorMaxLength.NameMin, AuthorMaxLength.NameMax)
                .WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}