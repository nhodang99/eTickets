using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services;

public class MoviesService(AppDbContext context) : EntityBaseRepository<Movie>(context), IMoviesService
{
   private readonly AppDbContext _context = context;

   public async Task AddNewMovieAsync(NewMovieVM data)
   {
      var newMovie = new Movie
      {
         Name = data.Name,
         Description = data.Description,
         Price = data.Price,
         ImageURL = data.ImageURL,
         StartDate = data.StartDate,
         EndDate = data.EndDate,
         MovieCategory = data.MovieCategory,
         ProducerId = data.ProducerId,
         CinemaId = data.CinemaId
      };
      await _context.Movies.AddAsync(newMovie);
      await _context.SaveChangesAsync();

      // Add Actors
      foreach (var actorId in data.ActorIds)
      {
         var newActorMovie = new Actor_Movie
         {
            MovieId = newMovie.Id,
            ActorId = actorId
         };
         await _context.Actors_Movies.AddAsync(newActorMovie);
      }
      await _context.SaveChangesAsync();
   }

   public async Task<Movie> getMovieByIdAsync(int id)
   {
      var movieDetails = await _context.Movies.Include(c => c.Cinema)
                                              .Include(p => p.Producer)
                                              .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                                              .FirstOrDefaultAsync(n => n.Id == id);

      return movieDetails;
   }

   public async Task<NewMovieDropdownVM> GetNewMovieDropdownValues()
   {
      var response = new NewMovieDropdownVM
      {
         Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
         Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
         Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
      };
      return response;
   }

   public async Task UpdateMovieAsync(NewMovieVM data)
   {
      var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
      if (dbMovie != null)
      {
         dbMovie.Name = data.Name;
         dbMovie.Description = data.Description;
         dbMovie.Price = data.Price;
         dbMovie.ImageURL = data.ImageURL;
         dbMovie.StartDate = data.StartDate;
         dbMovie.EndDate = data.EndDate;
         dbMovie.MovieCategory = data.MovieCategory;
         dbMovie.ProducerId = data.ProducerId;
         dbMovie.CinemaId = data.CinemaId;
         await _context.SaveChangesAsync();
      }

      // Remove existing actors
      var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
      _context.Actors_Movies.RemoveRange(existingActorsDb);
      await _context.SaveChangesAsync();

      // Add Actors
      if (data.ActorIds != null) // It cannot be null, but...
      {
         foreach (var actorId in data.ActorIds)
         {
            var newActorMovie = new Actor_Movie
            {
               MovieId = data.Id,
               ActorId = actorId
            };
            await _context.Actors_Movies.AddAsync(newActorMovie);
         }
         await _context.SaveChangesAsync();
      }
   }
}