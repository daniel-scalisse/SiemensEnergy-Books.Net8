using Books_Business.Core.Models;
using Books_Business.Modules.Books;
using System;
using System.Collections.Generic;

namespace Books_Business.Modules.Authors
{
    public struct AuthorMaxLength
    {
        public const byte NameMin = 4;
        public const byte NameMax = 50;
    }

    public class Author : Entity
    {
        public string Name { get; private set; }
        public DateTime InclusionDate { get; private set; }

        //Relationship
        public IEnumerable<Book> Books { get; set; }


        public Author()
        {

        }

        public Author(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new AuthorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}