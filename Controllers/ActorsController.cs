using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class ActorsController(AppDbContext context) : Controller
{
   private readonly AppDbContext _context = context;

   public async Task<IActionResult> Index()
   {
      var data = await _context.Actors.ToListAsync();
      return View(data);
   }
}