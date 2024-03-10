using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class LoginVM
{
   [Display(Name = "Email address")]
   [Required(ErrorMessage = "Email must not be empty")]
   public string Email { get; set; }

   [Required]
   [DataType(DataType.Password)]
   public string Password { get; set; }
}