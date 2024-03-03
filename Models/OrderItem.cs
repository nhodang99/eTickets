using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models;

public class OrderItem
{
   [Key]
   public int Id { get; set; }

   public int Amount { get; set; }
   public double Price { get; set; }

   public int MovieId { get; set; }

   [ForeignKey("MovieId")] // Is this equivalent to mark this property as virtual?
   public virtual Movie Movie { get; set; }

   public int OrderId { get; set; }

   [ForeignKey("OrderId")] // Is this equivalent to mark this property as virtual?
   public virtual Order Order { get; set; }
}