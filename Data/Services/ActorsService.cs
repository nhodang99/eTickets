using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services;

public class ActorsService(AppDbContext context) : EntityBaseRepository<Actor>(context), IActorsService
{
}