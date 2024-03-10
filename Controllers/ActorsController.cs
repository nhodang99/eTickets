using eTickets.Data.Base;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class ActorsController(IEntityBaseRepository<Actor> service) : Controller
{
   private readonly IEntityBaseRepository<Actor> _service = service;

   [AllowAnonymous]
   public async Task<IActionResult> Index()
   {
      var allActors = await _service.GetAllAsync();
      return View(allActors);
   }

   // GET: actors/create
   public IActionResult Create()
   {
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Actor actor)
   {
      if (!ModelState.IsValid)
      {
         return View(actor);
      }
      await _service.AddAsync(actor);
      return RedirectToAction(nameof(Index));
   }

   // GET: actors/details/id
   [AllowAnonymous]
   public async Task<IActionResult> Details(int id)
   {
      var actorDetails = await _service.GetByIdAsync(id);

      return (actorDetails == null) ? View("Empty") : View(actorDetails);
   }

   // Get: actors/edit/id
   public async Task<IActionResult> Edit(int id)
   {
      var actorDetails = await _service.GetByIdAsync(id);
      return actorDetails == null ? View("NotFound") : View(actorDetails);
   }

   [HttpPost]
   public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureURL, Bio")] Actor actor)
   {
      if (!ModelState.IsValid || id != actor.Id)
      {
         return View(actor);
      }
      await _service.UpdateAsync(actor);
      return RedirectToAction(nameof(Index));
   }

   // Get: actors/delete/id
   public async Task<IActionResult> Delete(int id)
   {
      var actorDetails = await _service.GetByIdAsync(id);
      return actorDetails == null ? View("NotFound") : View(actorDetails);
   }

   [HttpPost, ActionName("Delete")]
   public async Task<IActionResult> DeleteConfirm(int id)
   {
      var actorDetails = await _service.GetByIdAsync(id);
      if (actorDetails == null)
      {
         return View("NotFound");
      }

      await _service.DeleteAsync(id);
      return RedirectToAction(nameof(Index));
   }
}