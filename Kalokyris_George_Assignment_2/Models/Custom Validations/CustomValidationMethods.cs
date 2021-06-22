using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Kalokyris_George_Assignment_2.Models
{
    public class CustomValidationMethods
    {
        public static ValidationResult ValidateGreaterOrEqualToZero(double value, ValidationContext context)
        {
            bool isValid = true;

            if (value < 0)
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(string.Format($"The field {context.MemberName} must be greated or equal to 0"));
            }
        }

        
        public static ValidationResult ValidateHireDate(DateTime hireDate)
        {
            if(hireDate.Year > DateTime.Now.Year || hireDate.Year < 2010)
            {
                return new ValidationResult($"The hire date must be between 2010 and {DateTime.Now.Year}");
            }

            return ValidationResult.Success;
        }



    }
}