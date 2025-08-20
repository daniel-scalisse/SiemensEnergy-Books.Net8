using Books_Business.Modules.Genders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books_Api.ViewModels
{
    public class GenderInput
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(GenderMaxLength.NameMax, ErrorMessage = "The {0} field must be between {2} and {1} characters.", MinimumLength = GenderMaxLength.NameMin)]
        public string Name { get; set; }

        [DisplayName("Inclusion Date")]
        public DateTime? InclusionDate { get; set; }
    }

    public class GenderDetails
    {
        public GenderInput Gender { get; set; }
        public IEnumerable<BookView> Books { get; set; }
    }
}