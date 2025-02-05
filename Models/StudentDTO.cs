using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WebAPI_Learn.Validators;

namespace WebAPI_Learn.Models
{
    public class StudentDTO
    {
        [Range(10,20)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(20)]
        public string StudentName { get; set; }

        //[ValidateNever]
        [RollCheckValidator]
        public int Roll { get; set; }

        [Required(ErrorMessage = "Symbol is required")]
        public string Symbol { get; set; }

        //[EmailAddress]
        //public string Email { get; set; }

        public string Password { get; set; }

        [Compare(nameof(Password))] //compares the value of Password with ConfirmPassword
        public string ConfirmPassword { get; set; }

    }
}
