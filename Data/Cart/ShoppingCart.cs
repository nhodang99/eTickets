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

   public static ShoppingCart GetShoppingCart(IServiceProvider services)
   {
      ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
      var context = services.GetService<AppDbContext>();
      if (session is null || context is null)
      {
         throw new NullReferenceException("Http session or db context null");
      }

      var cartId = session.GetString("CardId") ?? Guid.NewGuid().ToString();
      session.SetString("CardId", cartId);
      return new ShoppingCart(context)
      {
         ShoppingCardId = cartId
      };
   }

   public async Task AddItemToCart(Movie movie)
   {
      var item = _context.ShoppingCartItems.FirstOrDefault(n => n.ShoppingCardId == ShoppingCardId && n.Movie.Id == movie.Id);
      if (item is null)
      {
         item = new ShoppingCartItem
         {
            ShoppingCardId = ShoppingCardId,
            Movie = movie,
            Amount = 1
         };
         _context.ShoppingCartItems.Add(item);
      }
      else
      {
         item.Amount++;
      }
      await _context.SaveChangesAsync();
   }

   public async Task RemoveItemFromCart(Movie movie)
   {
      var item = _context.ShoppingCartItems.FirstOrDefault(n => n.ShoppingCardId == ShoppingCardId && n.Movie.Id == movie.Id);
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
      await _context.SaveChangesAsync();
   }

   public async Task ClearShoppingCartAsync()
   {
      var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCardId == ShoppingCardId).ToListAsync();
      _context.ShoppingCartItems.RemoveRange(items);
      await _context.SaveChangesAsync();
   }
}