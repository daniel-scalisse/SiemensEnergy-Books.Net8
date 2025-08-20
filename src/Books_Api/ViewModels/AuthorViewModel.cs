using Books_Business.Modules.Authors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books_Api.ViewModels
{
    public class AuthorInput
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(AuthorMaxLength.NameMax, ErrorMessage = "The {0} field must be between {2} and {1} characters.", MinimumLength = AuthorMaxLength.NameMin)]
        public string Name { get; set; }

        [DisplayName("Inclusion Date")]
        public DateTime? InclusionDate { get; set; }
    }

    public class AuthorDetails
    {
        public AuthorInput Author { get; set; }
        public IEnumerable<BookView> Books { get; set; }
    }
}