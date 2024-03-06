using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class OrdersController(IMoviesService movieService,
                              IOrdersService orderService,
                              ShoppingCart shoppingCart) : Controller
{
   private readonly IMoviesService _movieService = movieService;
   private readonly IOrdersService _orderService = orderService;
   private readonly ShoppingCart _shoppingCart = shoppingCart;

   public IActionResult ShoppingCart()
   {
      var items = _shoppingCart.GetShoppingCartItems();
      _shoppingCart.ShoppingCartItems = items;

      var response = new ShoppingCartVM
      {
         ShoppingCart = _shoppingCart,
         ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
      };

      return View(response);
   }

   public async Task<IActionResult> AddItemToShoppingCart(int id)
   {
      var item = await _movieService.getMovieByIdAsync(id);

      if (item is not null)
      {
         await _shoppingCart.AddItemToCart(item);
      }
      return RedirectToAction(nameof(ShoppingCart));
   }

   public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
   {
      var item = await _movieService.getMovieByIdAsync(id);

      if (item is not null)
      {
         await _shoppingCart.RemoveItemFromCart(item);
      }
      return RedirectToAction(nameof(ShoppingCart));
   }

   public async Task<IActionResult> Index()
   {
      string userId = "";
      var orders = await _orderService.GetOrdersByUserIdAsync(userId);
      return View(orders);
   }

   public async Task<IActionResult> CompleteOrder()
   {
      var items = _shoppingCart.GetShoppingCartItems();
      if (items == null || items.Count == 0)
      {
         return RedirectToAction(nameof(ShoppingCart));
      }
      string userId = "";
      string userEmail = "";

      await _orderService.StoreOrderAsync(items, userId, userEmail);
      await _shoppingCart.ClearShoppingCartAsync();
      return View("OrderCompleted");
   }
}