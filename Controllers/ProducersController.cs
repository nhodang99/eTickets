using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class ProducersController(AppDbContext context) : Controller
{
   private readonly AppDbContext _context = context;

   public async Task<IActionResult> Index()
   {
      var data = await _context.Producers.ToListAsync();
      return View(data);
   }
}