using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class CinemasController(AppDbContext context) : Controller
{
   private readonly AppDbContext _context = context;

   public async Task<IActionResult> Index()
   {
      var data = await _context.Cinemas.ToListAsync();
      return View(data);
   }
}