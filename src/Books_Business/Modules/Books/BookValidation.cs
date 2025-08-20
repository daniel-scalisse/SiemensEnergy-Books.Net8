using FluentValidation;

namespace Books_Business.Modules.Books
{
    public class BookValidation : AbstractValidator<Book>
    {
        public BookValidation()
        {
            RuleFor(l => l.GenderId)
                .GreaterThan(0).WithMessage("The {PropertyName} field is required and must be greater than zero.");

            RuleFor(l => l.AuthorId)
                .GreaterThan(0).WithMessage("The {PropertyName} field is required and must be greater than zero.");

            RuleFor(l => l.Title)
                .NotEmpty().WithMessage("The {PropertyName} field is required.")
                .Length(BookMaxLength.TitleMin, BookMaxLength.TitleMax)
                .WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters.");

            RuleFor(l => (int)l.Year)
                .GreaterThan(0).WithMessage("The {PropertyName} field is required and must be greater than zero.");

            RuleFor(l => (int)l.Edition)
                .GreaterThan(0).WithMessage("The {PropertyName} field is required and must be greater than zero.");

            RuleFor(l => (int)l.PageQuantity)
                .GreaterThan(0).WithMessage("The Page Quantity field is required and must be greater than zero.");

            RuleFor(l => l.ISBN)
                .NotEmpty().WithMessage("The {PropertyName} field is required.")
                .Length(BookMaxLength.ISBNMin, BookMaxLength.ISBNMax)
                .WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters.");

            RuleFor(l => l.Barcode)
                .NotEmpty().WithMessage("The {PropertyName} field is required.")
                .Length(BookMaxLength.BarcodeMin, BookMaxLength.BarcodeMax)
                .WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters.");
        }
    }
}