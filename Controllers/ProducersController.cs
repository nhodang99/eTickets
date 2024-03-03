using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class ProducersController(IProducersService service) : Controller
{
   private readonly IProducersService _service = service;

   public async Task<IActionResult> Index()
   {
      var data = await _service.GetAllAsync();
      return View(data);
   }

   // GET: producers/detail/id
   public async Task<IActionResult> Details(int id)
   {
      var producerDetails = await _service.GetByIdAsync(id);
      return (producerDetails == null) ? View("NotFound") : View(producerDetails);
   }

   // GET: producers/create
   public IActionResult Create()
   {
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Producer producer)
   {
      if (!ModelState.IsValid)
      {
         return View(producer);
      }
      await _service.AddAsync(producer);
      return RedirectToAction(nameof(Index));
   }

   // GET: producers/edit/id
   public async Task<IActionResult> Edit(int id)
   {
      var producerDetails = await _service.GetByIdAsync(id);
      return (producerDetails == null) ? View("NotFound") : View(producerDetails);
   }

   [HttpPost]
   public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureURL, Bio")] Producer producer)
   {
      if (!ModelState.IsValid || id != producer.Id)
      {
         return View(producer);
      }
      await _service.UpdateAsync(producer);
      return RedirectToAction(nameof(Index));
   }

   // Get: actors/delete/id
   public async Task<IActionResult> Delete(int id)
   {
      var producerDetails = await _service.GetByIdAsync(id);
      return (producerDetails == null) ? View("NotFound") : View(producerDetails);
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