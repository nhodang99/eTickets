using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;

namespace eTickets.Models;

public class Cinema : IEntityBase
{
   [Key]
   public int Id { get; set; }

   [Required(ErrorMessage = "Cinema logo is required")]
   public string Logo { get; set; } = string.Empty;

   [Display(Name = "Cinema")]
   [Required(ErrorMessage = "Cinema name is required")]
   [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
   public string Name { get; set; } = string.Empty;

   [Display(Name = "Description")]
   [Required(ErrorMessage = "Description is required")]
   public string Description { get; set; } = string.Empty;

   // Relationships
   public List<Movie>? Movies { get; set; }
}
