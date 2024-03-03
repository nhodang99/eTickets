using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;

namespace eTickets.Models;

public class Producer : IEntityBase
{
   [Key]
   public int Id { get; set; }

   [Display(Name = "Profile Picture")]
   [Required(ErrorMessage = "Profile picture is required")]
   public string ProfilePictureURL { get; set; } = string.Empty;

   [Display(Name = "Full Name")]
   [Required(ErrorMessage = "Full name is required")]
   [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 50 characters")]
   public string FullName { get; set; } = string.Empty;

   [Display(Name = "Biography")]
   public string? Bio { get; set; }

   // Relationships
   public List<Movie>? Movies { get; set; }
}
