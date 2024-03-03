using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart;

public class ShoppingCart(AppDbContext context)
{
   public AppDbContext _context = context;

   public string ShoppingCardId { get; set; }

   public List<ShoppingCartItem> ShoppingCartItems { get; set; }

   public List<ShoppingCartItem> GetShoppingCartItems()
   {
      return ShoppingCartItems ??= [.. _context.ShoppingCartItems.Where(item => item.ShoppingCardId == ShoppingCardId)
                                                                  .Include(item => item.Movie)];
   }

   public double GetShoppingCartTotal() => _context.ShoppingCartItems.Where(item => item.ShoppingCardId == ShoppingCardId)
                                                                     .Select(item => item.Movie.Price * item.Amount)
                                                                     .Sum();

   public void AddItemToCart(Movie movie)
   {
      var item = _context.ShoppingCartItems.FirstOrDefault(item => item.ShoppingCardId == ShoppingCardId && item.Movie.Id == movie.Id);
      if (item is null)
      {
         item = new ShoppingCartItem
         {
            ShoppingCardId = ShoppingCardId,
            Movie = movie,
            Amount = 1
         };
      }
      else
      {
         item.Amount++;
      }
      _context.SaveChangesAsync();
   }

   public void RemoveItemFromCart(Movie movie)
   {
      var item = _context.ShoppingCartItems.FirstOrDefault(item => item.ShoppingCardId == ShoppingCardId && item.Movie.Id == movie.Id);
      if (item is not null)
      {
         if (item.Amount > 1)
         {
            item.Amount--;
         }
         else
         {
            _context.ShoppingCartItems.Remove(item);
         }
      }
      _context.SaveChangesAsync();
   }
}