using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Books_Api.Extensions
{
    //Decorate and validate via back-end.
    public class CurrencyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var currency = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult("Moeda no formato inválido!");
            }

            return ValidationResult.Success;
        }
    }
}