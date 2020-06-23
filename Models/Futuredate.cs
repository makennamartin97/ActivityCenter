  
using System.ComponentModel.DataAnnotations;
using System;

namespace csharpexam.Models
{
  public class FuturedateAttribute : ValidationAttribute
  {
      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
          if ((DateTime) value < DateTime.Today)
          {
              return new ValidationResult("Event date must be in the future.");
          }
          return ValidationResult.Success;
      }
  }
}