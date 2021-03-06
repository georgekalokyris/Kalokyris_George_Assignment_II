using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalokyris_George_Assignment_2.Models
{
    public class Trainer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Trainer ID")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Trainer's First Name is required")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
        [MaxLength(60, ErrorMessage = "First name cannot be over 60 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Trainer's Last Name is required")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        [MaxLength(60, ErrorMessage = "Last name cannot be over 60 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [CustomValidation(typeof(CustomValidationMethods), "ValidateGreaterOrEqualToZero")]
        public decimal Salary { get; set; }

        [Display(Name = "Hire Date")]
        [Required(ErrorMessage = "Hire Date is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [DataType(DataType.Date)]
        //[Range(typeof(DateTime), "1/1/2010", "1/1/2022", ErrorMessage = "Trainer's Hire Date must be between 2010 and 2021")] ////This was not working
        [CustomValidation(typeof(CustomValidationMethods), "ValidateHireDate")] //So I created this which evaluates dates between 2010 and now
        public DateTime HireDate { get; set; }

        [Display(Name = "Trainer Available")]
        public bool isAvailable { get; set; }

        //Navigation Properties
        public virtual ICollection<Category> Categories { get; set; }

    }
}