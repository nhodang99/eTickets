using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Data.Services;

public class OrdersServices(AppDbContext context) : IOrdersService
{
   private readonly AppDbContext _context = context;

   public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
   {
      var orders = await _context.Orders.Include(n => n.OrderItems)
                                          .ThenInclude(n => n.Movie)
                                          .Where(n => n.UserId == userId).ToListAsync();
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