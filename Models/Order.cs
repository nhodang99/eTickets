using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models;

public class Order
{
   [Key]
   public int Id { get; set; }

   public string Email { get; set; } = string.Empty;

   public string UserId { get; set; } = string.Empty;

   [ForeignKey("UserId")]
   public ApplicationUser User { get; set; }

   public List<OrderItem> OrderItems { get; set; }
}