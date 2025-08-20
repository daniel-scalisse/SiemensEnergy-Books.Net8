using Books_Api.Extensions;
using Books_Business.Modules.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books_Api.ViewModels
{
    public class BookInput
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public int GenderId { get; set; }

        [DisplayName("Author")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(BookMaxLength.TitleMax, ErrorMessage = "The {0} field must be between {2} and {1} characters.", MinimumLength = BookMaxLength.TitleMin)]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public int Edition { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [DisplayName("Page Quantity")]
        public int PageQuantity { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(BookMaxLength.ISBNMax, ErrorMessage = "The {0} field must be between {2} and {1} characters.", MinimumLength = BookMaxLength.ISBNMin)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(BookMaxLength.BarcodeMax, ErrorMessage = "The {0} field must be between {2} and {1} characters.", MinimumLength = BookMaxLength.BarcodeMin)]
        public string Barcode { get; set; }

        [Currency]
        public decimal? Value { get; set; }

        [DisplayName("Purchase date")]
        public DateTime? PurchaseDate { get; set; }

        public bool Dedication { get; set; }

        public string Observation { get; set; }

        //Upload Image
        [DisplayName("Cover image")]
        public string ImageUpload { get; set; }

        [DisplayName("Inclusion Date")]
        public DateTime? InclusionDate { get; set; }
    }

    public class BookView
    {
        public int Id { get; set; }
        public string GenderName { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int Year { get; set; }
        public int Edition { get; set; }
        public int PageQuantity { get; set; }
        public string ISBN { get; set; }
        public string Barcode { get; set; }

        [Currency]
        public decimal? Value { get; set; }

        public DateTime? PurchaseDate { get; set; }
        public bool Dedication { get; set; }
        public string Observation { get; set; }
        public string ImageUpload { get; set; }
        public DateTime InclusionDate { get; set; }
    }

    public class BookLists
    {
        public IEnumerable<KeyValuePair<int, string>> Genders { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Authors { get; set; }
    }

    public class BookEdit
    {
        public BookInput Book { get; set; }
        public BookLists BookLists { get; set; }
    }
}