
namespace eTickets.Models;

// Join table of many to many relation
public class Actor_Movie
{
   public int MovieId { get; set; }
   public Movie Movie { get; set; }

   public int ActorId { get; set; }
   public Actor Actor { get; set; }
}
