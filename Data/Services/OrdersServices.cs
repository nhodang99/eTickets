using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Data.Services;

public class OrdersServices(AppDbContext context) : IOrdersService
{
   private readonly AppDbContext _context = context;

   public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
   {
      var orders = await _context.Orders.Include(n => n.User)
                                          .Include(n => n.OrderItems).ThenInclude(n => n.Movie)
                                          .ToListAsync();
      if (userRole != UserRoles.Admin)
      {
         orders = orders.Where(n => n.UserId == userId).ToList();
      }
      return orders;
   }

   public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmail)
   {
      var order = new Order
      {
         // Id is created by DB
         UserId = userId,
         Email = userEmail
      };
      await _context.Orders.AddAsync(order);
      await _context.SaveChangesAsync();

      foreach (var item in items)
      {
         var orderItem = new OrderItem
         {
            Amount = item.Amount,
            MovieId = item.Movie.Id,
            OrderId = order.Id,
            Price = item.Movie.Price
         };
         await _context.OrderItems.AddAsync(orderItem);
      }
      await _context.SaveChangesAsync();
   }
}