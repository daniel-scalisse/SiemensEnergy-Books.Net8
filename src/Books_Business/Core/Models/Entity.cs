using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books_Business.Core.Models
{
    /// <summary>
    /// Entidade abstrata base para as entidades de negócio, que serão tratadas no repositório genérico.
    /// </summary>
    public abstract class Entity
    {
        public int Id { get; protected set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}