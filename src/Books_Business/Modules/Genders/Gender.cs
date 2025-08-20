using Books_Business.Core.Models;
using Books_Business.Modules.Books;
using System;
using System.Collections.Generic;

namespace Books_Business.Modules.Genders
{
    public struct GenderMaxLength
    {
        public const byte NameMin = 1;
        public const byte NameMax = 50;
    }

    public class Gender : Entity
    {
        public string Name { get; private set; }
        public DateTime InclusionDate { get; private set; }

        //Relationship
        public IEnumerable<Book> Books { get; set; }


        public Gender()
        {

        }

        public Gender(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new GenderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}