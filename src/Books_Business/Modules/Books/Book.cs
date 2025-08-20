using Books_Business.Core.Models;
using Books_Business.Modules.Authors;
using Books_Business.Modules.Genders;
using System;

namespace Books_Business.Modules.Books
{
    public struct BookMaxLength
    {
        public const byte TitleMin = 1;
        public const byte TitleMax = 100;
        public const byte SubTitleMin = 1;
        public const byte SubTitleMax = 200;
        public const byte ISBNMin = 10;
        public const byte ISBNMax = 20;
        public const byte BarcodeMin = 10;
        public const byte BarcodeMax = 30;
        public const byte Observation = 200;
    }

    public class Book : Entity
    {
        public int GenderId { get; private set; }
        public int AuthorId { get; private set; }
        public string Title { get; private set; }
        public string SubTitle { get; private set; }
        public Int16 Year { get; private set; }
        public Int16 Edition { get; private set; }
        public Int16 PageQuantity { get; private set; }
        public string ISBN { get; private set; }
        public string Barcode { get; private set; }
        public decimal? Value { get; private set; }
        public DateTime? PurchaseDate { get; private set; }
        public bool Dedication { get; private set; }
        public string Observation { get; private set; }
        public DateTime InclusionDate { get; private set; }


        //Relationship
        public Gender Gender { get; set; }
        public Author Author { get; set; }
        public BookImage BookImage { get; set; }

        public Book()
        {

        }

        public Book(int id, int genderId, int authorId, string title, string subTitle, short year, short edition, short pageQuantity, string isbn, string barcode, decimal? value, DateTime? purchaseDate, bool dedication, string observation)
        {
            Id = id;
            GenderId = genderId;
            AuthorId = authorId;
            Title = title;
            SubTitle = subTitle;
            Year = year;
            Edition = edition;
            PageQuantity = pageQuantity;
            ISBN = isbn;
            Barcode = barcode;
            Value = value;
            PurchaseDate = purchaseDate;
            Dedication = dedication;
            Observation = observation;
        }

        public override bool IsValid()
        {
            ValidationResult = new BookValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}