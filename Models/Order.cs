using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Order
{
   [Key]
   public int Id { get; set; }

   public string Email { get; set; } = string.Empty;

   public string UserId { get; set; } = string.Empty;

   public List<OrderItem> OrderItems { get; set; }
}