using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers;

public class MoviesController(IMoviesService service) : Controller
{
   private readonly IMoviesService _service = service;

   public async Task<IActionResult> Index()
   {
      var data = await _service.GetAllAsync(n => n.Cinema);
      return View(data);
   }

   public async Task<IActionResult> Filter(string searchString) // Must be the same name as in the razor page
   {
      var movies = await _service.GetAllAsync(n => n.Cinema);

      if (!string.IsNullOrEmpty(searchString))
      {
         var filteredResult = movies.Where(n => n.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                             || n.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                    .ToList();
         return View("index", filteredResult); // TODO: when back from this page, it shows error page because Filter page doesn't exist
      }

      return View("index", movies);
   }

   // GET: movies/details/id
   public async Task<IActionResult> Details(int id)
   {
      var movieDetails = await _service.getMovieByIdAsync(id);
      return View(movieDetails);
   }

   // GET: movies/create
   public async Task<IActionResult> Create()
   {
      var movieDropdown = await _service.GetNewMovieDropdownValues();
      ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
      ViewBag.producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
      ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Create(NewMovieVM movie)
   {
      if (!ModelState.IsValid)
      {
         var movieDropdown = await _service.GetNewMovieDropdownValues();
         ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
         ViewBag.producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
         ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");
         return View(movie);
      }
      await _service.AddNewMovieAsync(movie);
      return RedirectToAction(nameof(Index));
   }

   // GET: movies/edit/id
   public async Task<IActionResult> Edit(int id)
   {
      var movieDetails = await _service.getMovieByIdAsync(id);
      if (movieDetails == null)
      {
         return View("NotFound");
      }

      var response = new NewMovieVM
      {
         Id = movieDetails.Id,
         Name = movieDetails.Name,
         Description = movieDetails.Description,
         Price = movieDetails.Price,
         StartDate = movieDetails.StartDate,
         EndDate = movieDetails.EndDate,
         ImageURL = movieDetails.ImageURL,
         MovieCategory = movieDetails.MovieCategory,
         CinemaId = movieDetails.CinemaId,
         ProducerId = movieDetails.ProducerId,
         ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList()
      };

      var movieDropdown = await _service.GetNewMovieDropdownValues();
      ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
      ViewBag.producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
      ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");
      return View(response);
   }

   [HttpPost]
   public async Task<IActionResult> Edit(int id, NewMovieVM movie)
   {
      if (id != movie.Id)
      {
         return View("NotFound");
      }
      if (!ModelState.IsValid)
      {
         var movieDropdown = await _service.GetNewMovieDropdownValues();
         ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
         ViewBag.producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
         ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");
         return View(movie);
      }
      await _service.UpdateMovieAsync(movie);
      return RedirectToAction(nameof(Index));
   }
}