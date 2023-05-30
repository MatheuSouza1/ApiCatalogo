using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Validations
{
    public class PrimeiraLetraAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validation)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome deve ser maiuscula");
            }
            return ValidationResult.Success;
        }
    }
}
