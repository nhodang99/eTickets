using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class ShoppingCartItem
{
   [Key]
   public int Id { get; set; }

   public Movie Movie { get; set; }

   public int Amount { get; set; }

   // We can create ShoppingCart model instead
   public string ShoppingCardId { get; set; }
}