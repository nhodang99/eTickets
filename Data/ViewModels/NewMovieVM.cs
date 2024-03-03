using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class NewMovieVM
{
   public int Id { get; set; }

   [Display(Name = "Movie name")]
   [Required(ErrorMessage = "Movie name is required")]
   public string Name { get; set; } = string.Empty;

   [Display(Name = "Movie description")]
   [Required(ErrorMessage = "Movie description is required")]
   public string Description { get; set; } = string.Empty;

   [Display(Name = "Price in $")]
   [Required(ErrorMessage = "Movie price is required")]
   public double Price { get; set; }

   [Display(Name = "Movie poster URL")]
   [Required(ErrorMessage = "Movie poster URL is required")]
   public string ImageURL { get; set; } = string.Empty;

   [Display(Name = "Start date")]
   [Required(ErrorMessage = "Movie start date is required")]
   public DateTime StartDate { get; set; }

   [Display(Name = "End date")]
   [Required(ErrorMessage = "Movie End date is required")]
   public DateTime EndDate { get; set; }

   [Display(Name = "Select a category")]
   [Required(ErrorMessage = "Movie category is required")]
   public MovieCategory MovieCategory { get; set; }

   // Relationship
   // The ActorIds list must be nullable to achive invalid ModelState in field Actor(s)
   // Or declare it wihout initialize, but the intellisense will give a warning
   [Display(Name = "Select actor(s)")]
   [Required(ErrorMessage = "Movie actor(s) is required")]
   public List<int>? ActorIds { get; set; }

   [Display(Name = "Select a cinema")]
   [Required(ErrorMessage = "Cinema is required")]
   public int CinemaId { get; set; }

   [Display(Name = "Select a producer")]
   [Required(ErrorMessage = "Movie producer is required")]
   public int ProducerId { get; set; }
}
