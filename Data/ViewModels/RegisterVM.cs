using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class RegisterVM
{
   [Display(Name = "Full name")]
   [Required(ErrorMessage = "Full name must not be empty")]
   public string FullName { get; set; }

   [Display(Name = "Email address")]
   [Required(ErrorMessage = "Email must not be empty")]
   public string Email { get; set; }

   [Required]
   [DataType(DataType.Password)]
   public string Password { get; set; }

   [Display(Name = "Confirm password")]
   [Required(ErrorMessage = "Confirm password is required")]
   [DataType(DataType.Password)]
   [Compare("Password", ErrorMessage = "Password do not match")]
   public string ConfirmPassword { get; set; }
}