using eTickets.Models;

namespace eTickets.Data
{
   public class AppDbInitializer
   {
      public static void Seed(IApplicationBuilder applicationBuilder)
      {
         using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
         {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();

            //Cinema
            if (!context.Cinemas.Any())
            {
               context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new()
                        {
                            Name = "Cinema 1",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new()
                        {
                            Name = "Cinema 2",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new()
                        {
                            Name = "Cinema 3",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new()
                        {
                            Name = "Cinema 4",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-4.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new()
                        {
                            Name = "Cinema 5",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-5.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                    });
               context.SaveChanges();
            }
            //Actors
            if (!context.Actors.Any())
            {
               context.Actors.AddRange(new List<Actor>()
                    {
                        new()
                        {
                            FullName = "Actor 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-1.jpeg"

                        },
                        new()
                        {
                            FullName = "Actor 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg"
                        },
                        new()
                        {
                            FullName = "Actor 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-3.jpeg"
                        },
                        new()
                        {
                            FullName = "Actor 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-4.jpeg"
                        },
                        new()
                        {
                            FullName = "Actor 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-5.jpeg"
                        }
                    });
               context.SaveChanges();
            }
            //Producers
            if (!context.Producers.Any())
            {
               context.Producers.AddRange(new List<Producer>()
                    {
                        new()
                        {
                            FullName = "Producer 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-1.jpeg"

                        },
                        new()
                        {
                            FullName = "Producer 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-2.jpeg"
                        },
                        new()
                        {
                            FullName = "Producer 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-3.jpeg"
                        },
                        new()
                        {
                            FullName = "Producer 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-4.jpeg"
                        },
                        new()
                        {
                            FullName = "Producer 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-5.jpeg"
                        }
                    });
               context.SaveChanges();
            }
            //Movies
            if (!context.Movies.Any())
            {
               context.Movies.AddRange(new List<Movie>()
                    {
                        new()
                        {
                            Name = "Life",
                            Description = "This is the Life movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            CinemaId = 3,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new()
                        {
                            Name = "The Shawshank Redemption",
                            Description = "This is the Shawshank Redemption description",
                            Price = 29.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new()
                        {
                            Name = "Ghost",
                            Description = "This is the Ghost movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new()
                        {
                            Name = "Race",
                            Description = "This is the Race movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new()
                        {
                            Name = "Scoob",
                            Description = "This is the Scoob movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 1,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new()
                        {
                            Name = "Cold Soles",
                            Description = "This is the Cold Soles movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-8.jpeg",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 1,
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Drama
                        }
                    });
               context.SaveChanges();
            }
            //Actors & Movies
            if (!context.Actors_Movies.Any())
            {
               context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new()
                        {
                            ActorId = 3,
                            MovieId = 1
                        },

                         new()
                        {
                            ActorId = 1,
                            MovieId = 2
                        },
                         new()
                        {
                            ActorId = 4,
                            MovieId = 2
                        },

                        new()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                        new()
                        {
                            ActorId = 5,
                            MovieId = 3
                        },


                        new()
                        {
                            ActorId = 2,
                            MovieId = 4
                        },
                        new()
                        {
                            ActorId = 3,
                            MovieId = 4
                        },
                        new()
                        {
                            ActorId = 4,
                            MovieId = 4
                        },


                        new()
                        {
                            ActorId = 2,
                            MovieId = 5
                        },
                        new()
                        {
                            ActorId = 3,
                            MovieId = 5
                        },
                        new()
                        {
                            ActorId = 4,
                            MovieId = 5
                        },
                        new()
                        {
                            ActorId = 5,
                            MovieId = 5
                        },


                        new()
                        {
                            ActorId = 3,
                            MovieId = 6
                        },
                        new()
                        {
                            ActorId = 4,
                            MovieId = 6
                        },
                        new()
                        {
                            ActorId = 5,
                            MovieId = 6
                        },
                    });
               context.SaveChanges();
            }
         }
      }
   }
}