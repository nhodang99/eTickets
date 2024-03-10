using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class CinemasController(ICinemasService service) : Controller
{
   private readonly ICinemasService _service = service;

   [AllowAnonymous]
   public async Task<IActionResult> Index()
   {
      var data = await _service.GetAllAsync();
      return View(data);
   }

   // GET: cinemas/details/id
   [AllowAnonymous]
   public async Task<IActionResult> Details(int id)
   {
      var cinemaDetails = await _service.GetByIdAsync(id);
      return cinemaDetails == null ? View("NotFound") : View(cinemaDetails);
   }

   // GET: cinemas/create
   public IActionResult Create()
   {
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Create([Bind("Logo, Name, Description")] Cinema cinema)
   {
      if (!ModelState.IsValid)
      {
         return View(cinema);
      }
      await _service.AddAsync(cinema);
      return RedirectToAction(nameof(Index));
   }

   // Get: cinemas/edit/id
   public async Task<IActionResult> Edit(int id)
   {
      var cinemaDetails = await _service.GetByIdAsync(id);
      return cinemaDetails == null ? View("NotFound") : View(cinemaDetails);
   }

   [HttpPost]
   public async Task<IActionResult> Edit(int id, [Bind("Id, Logo, Name, Description")] Cinema cinema)
   {
      if (!ModelState.IsValid || id != cinema.Id)
      {
         return View(cinema);
      }
      await _service.UpdateAsync(cinema);
      return RedirectToAction(nameof(Index));
   }

   // Get: cinemas/delete/id
   public async Task<IActionResult> Delete(int id)
   {
      var cinemaDetails = await _service.GetByIdAsync(id);
      return cinemaDetails == null ? View("NotFound") : View(cinemaDetails);
   }

   [HttpPost, ActionName("Delete")]
   public async Task<IActionResult> DeleteConfirm(int id)
   {
      var cinemaDetails = await _service.GetByIdAsync(id);
      if (cinemaDetails == null)
      {
         return View("NotFound");
      }

      await _service.DeleteAsync(id);
      return RedirectToAction(nameof(Index));
   }
}